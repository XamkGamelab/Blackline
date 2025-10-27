using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    [UnityTest]
    public IEnumerator PlayerMovesForward()
    {
        var player = new GameObject("Player");
        var rb = player.AddComponent<Rigidbody>();

        rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);

        Assert.Greater(player.transform.position.z, 0.1f);
    }
}