using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("Detection")]
    [Tooltip("Transform of the player. If left empty, the script will try to find an object tagged \"Player\" at runtime.")]
    public Transform player;
    [Tooltip("How close the player must be to trigger the movement (world units).")]
    public float detectionRadius = 5f;

    [Header("Target")]
    [Tooltip("If true, the 'Target Position' is treated as an offset from the object's starting position (local offset). " +
             "If false, the 'Target Position' is interpreted as a world position.")]
    public bool targetIsLocal = true;
    [Tooltip("Target position or offset (depending on 'Target Is Local').")]
    public Vector3 targetPosition = Vector3.zero;

    [Header("Timing")]
    [Tooltip("How long (seconds) the object takes to move to the target.")]
    public float moveDuration = 1f;
    [Tooltip("How long (seconds) the object stays at the target before returning.")]
    public float returnDelay = 2f;

    [Header("Behavior")]
    [Tooltip("If true the object will move back to its starting position after 'returnDelay'.")]
    public bool returnToStart = true;
    [Tooltip("If true the trigger will only fire once. If false it can trigger every time the player re-enters range.")]
    public bool triggerOnce = false;

    // internal state
    private Vector3 startWorldPosition;
    private Vector3 startLocalPosition;
    private bool isMoving = false;
    private bool hasTriggered = false;

    void Start()
    {
        // keep startWorldPosition as the transform position (pivot) for movement calculations
        startWorldPosition = transform.position;
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (player == null)
        {
            var pgo = GameObject.FindGameObjectWithTag("Player");
            if (pgo != null) player = pgo.transform;
        }

        if (player == null) return;

        if (!isMoving && (!triggerOnce || !hasTriggered))
        {
            Vector3 detectionCenter = GetDetectionCenter();
            float dist = Vector3.Distance(player.position, detectionCenter);
            if (dist <= detectionRadius)
            {
                StartCoroutine(ProcessMove());
            }
        }
    }

    private IEnumerator ProcessMove()
    {
        if (isMoving) yield break;

        isMoving = true;
        hasTriggered = true;

        Vector3 desiredTargetWorld = targetIsLocal
            ? startWorldPosition + targetPosition
            : targetPosition;

        // Move from current position to target
        yield return StartCoroutine(Move(transform.position, desiredTargetWorld, moveDuration));

        if (returnToStart)
        {
            // Wait at target
            yield return new WaitForSeconds(returnDelay);

            // Move back to start
            yield return StartCoroutine(Move(transform.position, startWorldPosition, moveDuration));
        }

        isMoving = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to, float duration)
    {
        if (duration <= 0f)
        {
            transform.position = to;
            yield break;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Smoothstep easing
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }

        transform.position = to;
    }

    /// <summary>
    /// Returns the world-space center point to use for detection.
    /// Prefers any Collider bounds center (including children), then Renderer bounds center,
    /// otherwise falls back to transform.position.
    /// This keeps the detection sphere visually centered on the visible geometry.
    /// </summary>
    private Vector3 GetDetectionCenter()
    {
        // Try to find a collider on this object or in children
        Collider col = GetComponentInChildren<Collider>();
        if (col != null)
            return col.bounds.center;

        // Fallback to renderer bounds (mesh, sprite, etc.)
        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            return rend.bounds.center;

        // Last resort: object's transform position
        return transform.position;
    }

    void OnDrawGizmosSelected()
    {
        // draw detection radius centered on the object's geometry center when possible
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetDetectionCenter(), detectionRadius);

        // draw target line and point (keeps previous behavior: from pivot/start position)
        Vector3 startPos = Application.isPlaying ? startWorldPosition : transform.position;
        Vector3 targetWorld = targetIsLocal ? (startPos + targetPosition) : targetPosition;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, targetWorld);
        Gizmos.DrawSphere(targetWorld, 0.1f);
    }
}
