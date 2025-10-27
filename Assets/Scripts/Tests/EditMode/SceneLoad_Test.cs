using NUnit.Framework;
using UnityEngine;

public class SceneLoad_Test
{
    // Test-only subclass that hides the base method to avoid actually calling SceneManager.LoadScene in Edit Mode.
    private class TestableSceneLoadButton : SceneLoadButton
    {
        public int? LastSceneIndex { get; private set; }

        // Hide the base implementation so Edit Mode tests can verify the input without performing scene loads.
        public new void OnButtonPressed(int sceneIndex)
        {
            LastSceneIndex = sceneIndex;
        }
    }

    [Test]
    public void OnButtonPressed_WithValidIndex_RecordsIndex()
    {
        var go = new GameObject("SceneLoadButtonTest_Valid");
        var button = go.AddComponent<TestableSceneLoadButton>();

        int expectedIndex = 1;
        button.OnButtonPressed(expectedIndex);

        Assert.AreEqual(expectedIndex, button.LastSceneIndex, "OnButtonPressed should receive and record the provided valid index.");

        Object.DestroyImmediate(go);
    }

    [Test]
    public void OnButtonPressed_WithInvalidIndex_RecordsIndex()
    {
        var go = new GameObject("SceneLoadButtonTest_Invalid");
        var button = go.AddComponent<TestableSceneLoadButton>();

        int invalidIndex = -9999;
        button.OnButtonPressed(invalidIndex);

        Assert.AreEqual(invalidIndex, button.LastSceneIndex, "OnButtonPressed should receive and record the provided invalid index (EditMode test avoids actual load).");

        Object.DestroyImmediate(go);
    }
}
