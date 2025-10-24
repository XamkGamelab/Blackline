using System;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SceneLoadButtonTests
{
    [Test]
    public void OnButtonPressed_MethodExists_WithIntParameter()
    {
        var mi = typeof(SceneLoadButton).GetMethod("OnButtonPressed", BindingFlags.Public | BindingFlags.Instance);
        Assert.NotNull(mi, "Expected public instance method OnButtonPressed to exist.");
        var parameters = mi.GetParameters();
        Assert.AreEqual(1, parameters.Length, "OnButtonPressed should have exactly one parameter.");
        Assert.AreEqual(typeof(int), parameters[0].ParameterType, "OnButtonPressed parameter should be of type int.");
    }

    [Test]
    public void OnButtonPressed_ContainsCallToSceneManagerLoadScene()
    {
        var mi = typeof(SceneLoadButton).GetMethod("OnButtonPressed", BindingFlags.Public | BindingFlags.Instance);
        Assert.NotNull(mi, "Expected OnButtonPressed method to exist.");

        bool callsLoadScene = MethodCallsSceneManagerLoadScene(mi);
        Assert.IsTrue(callsLoadScene, "OnButtonPressed should call SceneManager.LoadScene(int).");
    }

    // Helper: lightweight IL scan to detect a 'call' instruction that resolves to SceneManager.LoadScene
    private static bool MethodCallsSceneManagerLoadScene(MethodInfo method)
    {
        var body = method.GetMethodBody();
        if (body == null) return false;
        var il = body.GetILAsByteArray();
        var module = method.Module;

        for (int i = 0; i < il.Length; i++)
        {
            byte op = il[i];

            // 0x28 is OpCodes.Call (single-byte opcode)
            if (op == 0x28)
            {
                // next 4 bytes = metadata token (little-endian)
                if (i + 4 >= il.Length) break;
                int token = BitConverter.ToInt32(il, i + 1);

                try
                {
                    var called = module.ResolveMethod(token);
                    if (called != null
                        && called.DeclaringType == typeof(UnityEngine.SceneManagement.SceneManager)
                        && called.Name.IndexOf("LoadScene", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Found a call to SceneManager.LoadScene (name matching)
                        return true;
                    }
                }
                catch
                {
                    // Ignore resolution failures and continue scanning
                }

                i += 4; // advance past the metadata token
            }
            else if (op == 0xFE)
            {
                // wide/opcode prefix - skip next byte (we don't fully parse all IL opcodes; we only search for 0x28 bytes)
                i++;
            }
            // Note: This is a pragmatic, minimal IL scan that looks for call opcodes.
            // It is intentionally lightweight to avoid executing SceneManager.LoadScene during tests.
        }

        return false;
    }
}
