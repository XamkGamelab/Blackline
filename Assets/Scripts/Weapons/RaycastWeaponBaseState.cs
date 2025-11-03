public abstract class RaycastWeaponBaseState
{
    // Finite State Machines are the fucking best. No they're not I hate you fuck you. -Shad //
    protected RaycastWeapon Weapon;
    public RaycastWeaponBaseState(RaycastWeapon weapon) => this.Weapon = weapon;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleUpdate() { }
    public virtual void HandleInput() { }
}
