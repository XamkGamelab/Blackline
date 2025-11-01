public class BaseWeapon : BaseItem
{
    public BaseWeaponDataSheet WeaponData => (BaseWeaponDataSheet)BaseItemDataSheet;

    // For things like shooting a weapon, or swinging an axe. -Shad //
    public virtual void PrimaryFunction() { }

    // For things like aiming a weapon, or perhaps throwing an axe. -Shad //
    public virtual void SecondaryFunction() { }

    // For things like cycling firing modes. -Shad //
    public virtual void ThirdFunction() { }

    public virtual void HandleUpdate() { }
}
