using UnityEngine;

public class MeleeWeaponLeftSwingState : WeaponState<MeleeWeapon>
{
    public MeleeWeaponLeftSwingState(MeleeWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Weapon.PrimaryFunction();
    }

    public override void HandleUpdate()
    {
        if (Time.time < Weapon.NextSwingTime) return;

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
        {
            Weapon.StateMachine.UpdateState(Weapon.RightSwingState);
        }
        else
        {
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
        }
    }
}
