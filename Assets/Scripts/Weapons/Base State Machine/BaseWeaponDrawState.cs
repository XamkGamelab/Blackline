using UnityEngine;

public class BaseWeaponDrawState : WeaponState<BaseWeapon>
{
    public BaseWeaponDrawState(BaseWeapon weapon) : base(weapon) { }

    private float _drawTimer = 0f;

    public override void HandleUpdate()
    {
        if (_drawTimer >= Weapon.WeaponData.HolsterTime)
        {
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
        }

        _drawTimer += Time.deltaTime;
    }

    public override void Exit()
    {
        _drawTimer = 0f;
    }
}
