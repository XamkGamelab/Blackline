using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    [Header("Melee Weapon References")]
    [SerializeField]
    private Transform _swingRaycastPosition;
    [SerializeField]
    private LayerMask _raycastLayers;    

    // Cast BaseWeaponDataSheet as MeleeWeaponDataSheet. -Shad //
    private MeleeWeaponDataSheet _dataSheet => (MeleeWeaponDataSheet)WeaponData;
    public MeleeWeaponDataSheet DataSheet => _dataSheet;

    // Melee Weapon specific states, base states from BaseWeapon. -Shad //
    public MeleeWeaponLeftSwingState LeftSwingState;
    public MeleeWeaponRightSwingState RightSwingState;
    public MeleeWeaponHeavySwingState HeavySwingState;

    // Play-time weapon data. -Shad //
    public float NextSwingTime { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        IdleState = new MeleeWeaponIdleState(this);
        LeftSwingState = new MeleeWeaponLeftSwingState(this);
        RightSwingState = new MeleeWeaponRightSwingState(this);
        HeavySwingState = new MeleeWeaponHeavySwingState(this);

        NextSwingTime = Time.time;

        StateMachine.UpdateState(DrawState);
    }

    public override void HandleFunctions()
    {
        base.HandleFunctions();

        SecondaryFunction();
        ThirdFunction();
    }

    #region Main Functions
    public override void PrimaryFunction()
    {
        NextSwingTime = Time.time + _dataSheet.SwingCooldown;

        Ray swingRay = new(_swingRaycastPosition.position, _swingRaycastPosition.forward);
        RaycastHit swingHit = new RaycastHit();

        // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
        if (Physics.Raycast(swingRay, out swingHit, _dataSheet.SwingMaxRange, _raycastLayers))
        {
            SurfaceImpactLibrary.Instance.SpawnImpactFX(swingHit); // Impact FX! -Shad //
        }

        // Sound effects. -Shad //
        WeaponAudio.PlayOnce(DataSheet.SwingSounds[0]);

        base.PrimaryFunction();
    }

    public override void SecondaryFunction()
    {
        // HEAVY ATTACK LOGIC HERE!! -Shad //

        base.SecondaryFunction();
    }
    #endregion
}
