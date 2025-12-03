using UnityEngine;

public class MeleeWeaponAttackState : WeaponState<MeleeWeapon>
{
    public MeleeWeaponAttackState(MeleeWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        //Weapon.SetSwingIndex(1);
    }

    public override void HandleUpdate()
    {
        if (Time.time < Weapon.NextSwingTime) return;

        if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
        {
            //Weapon.StateMachine.UpdateState(Weapon.RightSwingState);
            Weapon.SetSwingIndex(2);
        }
        else
        {
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
            Weapon.SetSwingIndex(0);
        }
    }
}
