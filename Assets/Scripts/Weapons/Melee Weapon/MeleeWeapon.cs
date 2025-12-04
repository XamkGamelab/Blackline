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
    public float NextAttackTime { get; private set; }
    public float CurrentAttackBufferTime { get; private set; }
    public int CurrentAnimIndex { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        IdleState = new MeleeWeaponIdleState(this);
        AttackState = new MeleeWeaponAttackState(this);

        NextAttackTime = Time.time;

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

        AddAttackCooldown();
        AddSwingIndex();

        Ray swingRay = new(_swingRaycastPosition.position, _swingRaycastPosition.forward);
        RaycastHit swingHit = new RaycastHit();

        // If it hits something, use the hit position. Else, use a dummy position if the player shoots in the sky for example. -Shad //
        if (Physics.Raycast(swingRay, out swingHit, _dataSheet.SwingMaxRange, _raycastLayers))
        {
            SurfaceImpactLibrary.Instance.SpawnImpactFX(swingHit); // Impact FX! -Shad //

            // The hit object has an IDamageble interface, so let's actually damage that entity. -Shad //
            if (swingHit.collider.TryGetComponent<IDamageble>(out IDamageble damageble))
            {
                damageble.ApplyDamage(DataSheet.AttackDamage, DataSheet.ArmorPenetration);
            }
        }

        // Sound effects. -Shad //
        AudioClip chosenClip = DataSheet.SwingSounds[Random.Range(0, DataSheet.SwingSounds.Length)];
        WeaponAudio.PlayOnce(chosenClip);
    }

    public void AddAttackCooldown()
    {
        float coolDown = WeaponAnimator.GetInteger("SwingIndex") < 3 ? DataSheet.LightAttackCooldown : DataSheet.HeavyAttackCooldown;

        NextAttackTime = Time.time + coolDown;
        CurrentAttackBufferTime = NextAttackTime + DataSheet.AttackInputBufferTime;
    }

    public void AddSwingIndex()
    {
        if (CurrentAnimIndex == 3) CurrentAnimIndex = 1;
        else CurrentAnimIndex++;

        //WeaponAnimator.SetInteger("SwingIndex", CurrentAnimIndex);
    }

    public void ResetSwingIndex()
    {
        CurrentAnimIndex = 0;
        //WeaponAnimator.SetInteger("SwingIndex", CurrentAnimIndex);
    }

    public override void SecondaryFunction()
    {
        // HEAVY ATTACK LOGIC HERE!! -Shad //
        base.SecondaryFunction();
    }
    #endregion
}
