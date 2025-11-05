using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    //Start button, with field if you want to quickly change the scene name for testing -Veeti//
    // Good job Veeti. -Shad //
    public void OnButtonPressed(int sceneIndex)
    {
        Time.timeScale = 1f; // Fucking nasty fucking hack. -Shad //
        SceneManager.LoadScene(sceneIndex);
    }
}