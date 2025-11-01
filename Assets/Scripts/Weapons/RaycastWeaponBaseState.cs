public abstract class RaycastWeaponBaseState
{
    protected RaycastWeapon Weapon;
    public RaycastWeaponBaseState(RaycastWeapon weapon) => this.Weapon = weapon;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleUpdate() { }
    public virtual void HandleInput() { }
}
