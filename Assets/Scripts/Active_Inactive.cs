using System.Collections.Generic;
using UnityEngine;

public class Active_Inactive : MonoBehaviour
{
    // List of GameObjects to be set inactive when the game is paused (timeScale == 0)
    [Tooltip("GameObjects to set inactive when the game is paused (timeScale == 0)")]
    public List<GameObject> objectsToSetInactive = new List<GameObject>();

    // Track if we've already set objects inactive to avoid repeated calls
    private bool objectsAreInactive = false;

    void Update()
    {
        if (Time.timeScale == 0 && !objectsAreInactive)
        {
            foreach (var obj in objectsToSetInactive)
            {
                if (obj != null && obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            }
            objectsAreInactive = true;
        }
        else if (Time.timeScale != 0 && objectsAreInactive)
        {
            foreach (var obj in objectsToSetInactive)
            {
                if (obj != null && !obj.activeSelf)
                {
                    obj.SetActive(true);
                }
            }
            objectsAreInactive = false;
        }
    }
}
