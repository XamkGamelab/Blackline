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

    private bool _readyToSwitch = false;
    public bool ReadyToSwitch => _readyToSwitch;

    private WeaponAudio _weaponAudio;
    public WeaponAudio WeaponAudio => _weaponAudio;

    public virtual void Initialize()
    {
        StateMachine = new WeaponStateMachine<BaseWeapon>();

        IdleState = new BaseWeaponIdleState(this);
        DrawState = new BaseWeaponDrawState(this);
        HolsterState = new BaseWeaponHolsterState(this);
    }

    public virtual void HandleFunctions()
    {
        StateMachine?.HandleStates();
    }

    #region Main Functionality
    // For things like shooting a weapon, or swinging an axe. -Shad //
    public virtual void PrimaryFunction() { }

    // For things like aiming a weapon, or perhaps throwing an axe. -Shad //
    public virtual void SecondaryFunction() { }

    // For things like cycling firing modes. -Shad //
    public virtual void ThirdFunction() { }
    #endregion

    #region Drawing & Holstering
    // Drawing and equipping the weapon. -Shad //
    public virtual void DrawWeapon()
    {

    }

    // Holstering the weapon away. -Shad //
    public virtual void HolsterWeapon()
    {

    }
    #endregion

    #region Utility
    // Inventory scripts can set the provider with this method. -Shad //
    public void SetAmmoProvider(IAmmoProvider ammoProvider) => _ammoProvider = ammoProvider;

    public void SetAudioSource(WeaponAudio weaponAudio)
    {
        _weaponAudio = weaponAudio;
    }

    public void SetSwitchReady(bool target) => _readyToSwitch = target;
    #endregion
}
