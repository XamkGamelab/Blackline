using UnityEngine;

[System.Serializable]
public class PlayerSettingsData
{
    #region Keybinds
    [Header("Keybinds")]
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
    public KeyCode UniqueWeaponAction = KeyCode.Q; // Cycle firemode, charge plasma, grenade launchers, switch homing to cluster etc... -Shad //
    public KeyCode AkimboKey = KeyCode.T; // Absolute fucking buffoon. Fuck you Shad. -Shad //

    // Inventory Interaction //
    public KeyCode MeleeKey = KeyCode.Alpha1;
    public KeyCode LightCategoryKey = KeyCode.Alpha2;
    public KeyCode MediumCategoryKey = KeyCode.Alpha3;
    public KeyCode HeavyCategoryKey = KeyCode.Alpha4;
    public KeyCode PlasmaCategoryKey = KeyCode.Alpha5;
    public KeyCode RocketCategoryKey = KeyCode.Alpha6;
    public KeyCode UtilityCategoryKey = KeyCode.G;
    public KeyCode ThrowableCategoryKey = KeyCode.V;

    // Utils //
    public KeyCode PauseKey = KeyCode.Escape;
    #endregion

    [Space]

    #region Values
    [Header("Mouse")]
    public float MouseSensitivity = 175f;
    public float AimSensitivityMultiplier = 0.8f;
    #endregion
}
