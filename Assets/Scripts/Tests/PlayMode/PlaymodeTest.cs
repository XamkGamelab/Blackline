using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlaymodeTest
{
    // Simple helper to record collisions
    private class CollisionRecorder : MonoBehaviour
    {
        public bool collided;
        void OnCollisionEnter(Collision _) => collided = true;
    }

    [UnityTest]
    public IEnumerator Rigidbody_Falls_And_Collides_With_Ground()
    {
        // Arrange
        var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "TestGround";
        ground.transform.position = new Vector3(0f, -0.5f, 0f);
        ground.transform.localScale = new Vector3(10f, 1f, 10f);
        var groundRenderer = ground.GetComponent<Renderer>();
        if (groundRenderer != null) groundRenderer.enabled = false;

        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "FallingSphere";
        sphere.transform.position = new Vector3(0f, 2f, 0f);
        var rb = sphere.AddComponent<Rigidbody>();
        rb.useGravity = true;
        var recorder = sphere.AddComponent<CollisionRecorder>();

        // Act — wait up to a reasonable number of fixed updates for the collision
        const int maxFixedSteps = 200; // ~4s with default fixedDeltaTime = 0.02
        int steps = 0;
        while (!recorder.collided && steps < maxFixedSteps)
        {
            steps++;
            yield return new WaitForFixedUpdate();
        }

        // Assert
        Assert.IsTrue(recorder.collided, $"Expected collision within {maxFixedSteps} fixed updates.");

        // Cleanup
        Object.Destroy(sphere);
        Object.Destroy(ground);
        yield return null;
    }
}
