using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField, Tooltip("The GameObject to activate after the delay.")]
    private GameObject target;

    [SerializeField, Tooltip("Delay in seconds before activating the target.")]
    private float delaySeconds = 1f;

    [SerializeField, Tooltip("If true the timer will start automatically in Start().")]
    private bool startOnStart = true;

    [SerializeField, Tooltip("If true the target will be set inactive at Start() if it isn't already.")]
    private bool ensureInactiveAtStart = false;

    private Coroutine runningCoroutine;

    private void Start()
    {
        if (ensureInactiveAtStart && target != null)
            target.SetActive(false);

        if (startOnStart)
            StartTimer();
    }
    public void StartTimer()
    {
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);

        runningCoroutine = StartCoroutine(ActivateAfterDelay());
    }
    public void CancelTimer()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }

    private IEnumerator ActivateAfterDelay()
    {
        if (target == null)
            yield break;

        if (delaySeconds > 0f)
            yield return new WaitForSeconds(delaySeconds);
        else
            yield return null;

        target.SetActive(true);
        runningCoroutine = null;
    }
}
