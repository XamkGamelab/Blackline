public abstract class BaseWeaponState
{
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleUpdate() { }
    public virtual void HandleInput() { }

    public abstract string WeaponAnimKeyword { get; }
    public abstract string ArmsAnimKeyword { get; }
}
