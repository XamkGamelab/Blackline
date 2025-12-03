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
    public MeleeWeaponAttackState AttackState;

    // Play-time weapon data. -Shad //
    public float NextSwingTime { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        IdleState = new MeleeWeaponIdleState(this);
        AttackState = new MeleeWeaponAttackState(this);

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
        base.PrimaryFunction();
    }

    // Called from MeleeWeaponEvents!! -Shad //
    public void SwingWeapon()
    {
        PrimaryFunction();

        Ray swingRay = new(_swingRaycastPosition.position, _swingRaycastPosition.forward);
        RaycastHit swingHit = new RaycastHit();

        // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
        if (Physics.Raycast(swingRay, out swingHit, _dataSheet.SwingMaxRange, _raycastLayers))
        {
            SurfaceImpactLibrary.Instance.SpawnImpactFX(swingHit); // Impact FX! -Shad //
        }

        // Sound effects. -Shad //

        AudioClip chosenClip = DataSheet.SwingSounds[Random.Range(0, DataSheet.SwingSounds.Length)];
        WeaponAudio.PlayOnce(chosenClip);
    }

    public void SetSwingIndex(int index)
    {
        NextSwingTime = Time.time + _dataSheet.SwingCooldown;

        WeaponAnimator.SetInteger("SwingIndex", index);
    }

    public override void SecondaryFunction()
    {
        // HEAVY ATTACK LOGIC HERE!! -Shad //

        base.SecondaryFunction();
    }
    #endregion
}
