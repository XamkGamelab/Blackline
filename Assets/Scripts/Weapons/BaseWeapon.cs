using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    private BaseWeaponDataSheet _baseWeaponDataSheet;
    public BaseWeaponDataSheet WeaponData => _baseWeaponDataSheet;

    private IAmmoProvider _ammoProvider;
    public IAmmoProvider AmmoProvider => _ammoProvider;

    public WeaponStateMachine<BaseWeapon> StateMachine { get; private set; }

    public WeaponState<BaseWeapon> IdleState { get; protected set; }
    public WeaponState<BaseWeapon> DrawState { get; protected set; }
    public WeaponState<BaseWeapon> HolsterState { get; protected set; }

    public virtual void Initialize()
    {
        StateMachine = new WeaponStateMachine<BaseWeapon>();

        IdleState = new BaseWeaponIdleState(this);
        //DrawState = new WeaponDrawState(this);
        //HolsterState = new WeaponHolsterState(this);
    }

    public virtual void HandleUpdate()
    {
        StateMachine?.HandleUpdate();
    }

    // Inventory scripts can set the provider with this method. -Shad //
    public void SetAmmoProvider(IAmmoProvider ammoProvider) => _ammoProvider = ammoProvider;

    // For things like shooting a weapon, or swinging an axe. -Shad //
    public virtual void PrimaryFunction() { }

    // For things like aiming a weapon, or perhaps throwing an axe. -Shad //
    public virtual void SecondaryFunction() { }

    // For things like cycling firing modes. -Shad //
    public virtual void ThirdFunction() { }

    // Drawing and equipping the weapon. -Shad //
    public virtual void DrawWeapon()
    {

    }

    // Holstering the weapon away. -Shad //
    public virtual void HolsterWeapon()
    {

    }
}
