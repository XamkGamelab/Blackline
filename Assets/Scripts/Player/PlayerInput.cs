using UnityEngine;

public class PlayerInput
{
    public static float GetAxis(string axisName)
    {
        switch (axisName)
        {
            case "Horizontal":
                float x = 0f;
                if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.StrafeLeftKey)) x -= 1f;
                if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.StrafeRightKey)) x += 1f;
                return Mathf.Clamp(x, -1f, 1f);

            case "Vertical":
                float z = 0f;
                if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.BackwardKey)) z -= 1f;
                if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ForwardKey)) z += 1f;
                return Mathf.Clamp(z, -1f, 1f);
        }

        return 0f;
    }
}
