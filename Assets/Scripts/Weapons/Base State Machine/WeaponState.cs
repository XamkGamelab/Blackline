public abstract class WeaponState<TWeapon> : BaseWeaponState where TWeapon : BaseWeapon
{
    // Finite State Machines are the fucking best. No they're not I hate you fuck you. -Shad //
    protected TWeapon Weapon { get; private set; }

    protected WeaponState(TWeapon weapon) => this.Weapon = weapon;
}
