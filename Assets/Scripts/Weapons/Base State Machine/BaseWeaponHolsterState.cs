using UnityEngine;

public class BaseWeaponHolsterState : WeaponState<BaseWeapon>
{
    public BaseWeaponHolsterState(BaseWeapon weapon) : base(weapon) { }

    private float _holsterTimer = 0f;

    public override void Enter()
    {
        Weapon.SetSwitchReady(false);
    }

    public override void HandleUpdate()
    {
        if (_holsterTimer >= Weapon.WeaponData.HolsterTime)
        {
            Weapon.SetSwitchReady(true);
        }

        _holsterTimer += Time.deltaTime;
    }

    public override void Exit()
    {
        _holsterTimer = 0f;
    }
}
