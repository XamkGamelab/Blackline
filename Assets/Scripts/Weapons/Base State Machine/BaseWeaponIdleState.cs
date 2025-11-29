public class BaseWeaponIdleState : WeaponState<BaseWeapon>
{
    public BaseWeaponIdleState(BaseWeapon weapon) : base(weapon) { }

    public override string WeaponAnimKeyword => "";
    public override string ArmsAnimKeyword => "Idle";
}
