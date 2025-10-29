using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class GravityTest
{
    [Test]
    public void Rigidbody_Falls_When_Gravity_Enabled()
    {
        // Arrange
        var originalAutoSim = Physics.autoSimulation;
        var originalGravity = Physics.gravity;
        var fixedDelta = Time.fixedDeltaTime;
        Physics.autoSimulation = false;

        var go = new GameObject("GravityTestObject");
        var rb = go.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        go.transform.position = new Vector3(0f, 10f, 0f);
        var initialY = go.transform.position.y;

        try
        {
            // Act - simulate several fixed steps (deterministic in Edit Mode)
            for (int i = 0; i < 50; i++)
                Physics.Simulate(fixedDelta);

            // Assert - object should have moved down
            Assert.Less(go.transform.position.y, initialY, "Rigidbody should have moved down under gravity.");
        }
        finally
        {
            Object.DestroyImmediate(go);
            Physics.gravity = originalGravity;
            Physics.autoSimulation = originalAutoSim;
        }
    }

    [Test]
    public void Rigidbody_DoesNotFall_When_Gravity_Disabled()
    {
        // Arrange
        var originalAutoSim = Physics.autoSimulation;
        var originalGravity = Physics.gravity;
        var fixedDelta = Time.fixedDeltaTime;
        Physics.autoSimulation = false;
        Physics.gravity = Vector3.zero;

        var go = new GameObject("NoGravityTestObject");
        var rb = go.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        go.transform.position = new Vector3(0f, 10f, 0f);
        var initialY = go.transform.position.y;

        try
        {
            // Act - simulate several fixed steps
            for (int i = 0; i < 50; i++)
                Physics.Simulate(fixedDelta);

            // Assert - position should remain effectively the same (allow tiny epsilon)
            Assert.That(go.transform.position.y, Is.EqualTo(initialY).Within(1e-4f));
        }
        finally
        {
            Object.DestroyImmediate(go);
            Physics.gravity = originalGravity;
            Physics.autoSimulation = originalAutoSim;
        }
    }

    [UnityTest]
    public IEnumerator GravityTestWithEnumeratorPasses()
    {
        // Simple enumerator test showing one-step simulation in Edit Mode
        var originalAutoSim = Physics.autoSimulation;
        var fixedDelta = Time.fixedDeltaTime;
        Physics.autoSimulation = false;

        var go = new GameObject("EnumeratorGravityObject");
        var rb = go.AddComponent<Rigidbody>();
        rb.useGravity = true;
        go.transform.position = new Vector3(0f, 5f, 0f);
        var initialY = go.transform.position.y;

        try
        {
            Physics.Simulate(fixedDelta);
            yield return null; // allow test runner to proceed
            Assert.Less(go.transform.position.y, initialY);
        }
        finally
        {
            Object.DestroyImmediate(go);
            Physics.autoSimulation = originalAutoSim;
        }
    }
}
