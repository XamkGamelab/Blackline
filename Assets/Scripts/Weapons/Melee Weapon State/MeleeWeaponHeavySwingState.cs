using UnityEngine;

public class MeleeWeaponHeavySwingState : WeaponState<MeleeWeapon>
{
    public MeleeWeaponHeavySwingState(MeleeWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Weapon.PrimaryFunction();
    }

    public override void HandleUpdate()
    {
        if (Time.time < Weapon.NextSwingTime) return;

        if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
        {
            Weapon.StateMachine.UpdateState(Weapon.HeavySwingState);
        }
        else
        {
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
        }
    }
}
