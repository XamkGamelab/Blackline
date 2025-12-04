using UnityEngine;

public class MeleeWeaponAttackState : WeaponState<MeleeWeapon>
{
    public MeleeWeaponAttackState(MeleeWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Weapon.PrimaryFunction();
    }

    public override void HandleUpdate()
    {
        if (Time.time < Weapon.NextAttackTime) return;

        if(Time.time < Weapon.CurrentAttackBufferTime)
        {
            if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
            {
                Weapon.StateMachine.UpdateState(Weapon.AttackState);
            }
        }
        else
        {
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
        }
    }
}
