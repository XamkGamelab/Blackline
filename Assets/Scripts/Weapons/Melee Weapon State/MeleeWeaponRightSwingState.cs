using UnityEngine;

public class MeleeWeaponRightSwingState : WeaponState<MeleeWeapon>
{
    public MeleeWeaponRightSwingState(MeleeWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Weapon.PrimaryFunction();
    }

    public override void HandleUpdate()
    {
        if (Time.time < Weapon.NextSwingTime) return;

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
        {
            Weapon.StateMachine.UpdateState(Weapon.HeavySwingState);
        }
        else
        {
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
        }
    }
}
