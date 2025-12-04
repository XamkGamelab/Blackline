using UnityEngine;

public class MeleeWeaponIdleState : BaseWeaponIdleState
{
    private MeleeWeapon _meleeWeapon => (MeleeWeapon)Weapon;

    public MeleeWeaponIdleState(BaseWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        base.Enter();

        _meleeWeapon.ResetSwingIndex();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
        {
            Weapon.StateMachine.UpdateState(_meleeWeapon.AttackState);
        }
    }
}
