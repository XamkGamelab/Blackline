using UnityEngine;

[System.Serializable]
public class PlayerSettingsData
{
    #region Keybinds
    // Movement //
    public KeyCode ForwardKey = KeyCode.W;
    public KeyCode BackwardKey = KeyCode.S;
    public KeyCode StrafeLeftKey = KeyCode.A;
    public KeyCode StrafeRightKey = KeyCode.D;
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode RunKey = KeyCode.LeftShift;
    public KeyCode CrouchKey = KeyCode.C;

    // Weapon Interaction //
    public KeyCode ShootKey = KeyCode.Mouse0;
    public KeyCode AimKey = KeyCode.Mouse1;
    public KeyCode ReloadKey = KeyCode.R;
    public KeyCode WeaponAction = KeyCode.Q;

    // Inventory Interaction //
    public KeyCode MeleeKey = KeyCode.Alpha1;
    public KeyCode PistolKey = KeyCode.Alpha2;
    public KeyCode MachinePistolKey = KeyCode.Alpha3;
    public KeyCode AssaultRifleKey = KeyCode.Alpha4;
    public KeyCode MinigunKey = KeyCode.Alpha5;
    public KeyCode FlakCannonKey = KeyCode.Alpha6;
    public KeyCode SniperKey = KeyCode.Alpha7;
    public KeyCode RocketLauncherKey = KeyCode.Alpha8;

    // Utils //
    public KeyCode PauseKey = KeyCode.Escape;
    #endregion

    #region Values
    public float MouseSensitivity = 175f;
    public float AimSensitivityMultiplier = 0.8f;
    #endregion
}
