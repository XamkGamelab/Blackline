public class BaseWeaponIdleState : WeaponState<BaseWeapon>
{
    public BaseWeaponIdleState(BaseWeapon weapon) : base(weapon) { }

    public override string WeaponAnimKeyword => "Idle";
    public override string ArmsAnimKeyword => "Idle";
}
