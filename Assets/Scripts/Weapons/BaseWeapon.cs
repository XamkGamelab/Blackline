using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [Header("Base Weapon References")]
    [SerializeField]
    private BaseWeaponDataSheet _baseWeaponDataSheet;
    public BaseWeaponDataSheet WeaponData => _baseWeaponDataSheet;
    [SerializeField]
    private Transform _leftHandTargetIK;
    public Transform LeftHandTargetIK => _leftHandTargetIK;
    [SerializeField]
    private Transform _rightHandTargetIK;
    public Transform RightHandTargetIK => _rightHandTargetIK;

    private IAmmoProvider _ammoProvider;
    public IAmmoProvider AmmoProvider => _ammoProvider;

    public WeaponStateMachine<BaseWeapon> StateMachine { get; private set; }

    public WeaponState<BaseWeapon> IdleState { get; protected set; }
    public WeaponState<BaseWeapon> DrawState { get; protected set; }
    public WeaponState<BaseWeapon> HolsterState { get; protected set; }

    public bool ReadyToSwitch { get; private set; }

    private WeaponAudio _weaponAudio;
    public WeaponAudio WeaponAudio => _weaponAudio;

    public float PlayerMovementSpeed { get; private set; }
    public bool PlayerAirborne { get; private set; }

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

    public void SetSwitchReady(bool target) => ReadyToSwitch = target;
    #endregion

    #region Animation
    public virtual string WeaponAnimAction()
    {
        return "";
    }

    public virtual string PlayerAnimAction()
    {
        return "";
    }

    public void SetPlayerMovementSpeed(float playerMoveVector) => PlayerMovementSpeed = playerMoveVector;

    public void SetPlayerAirborne(bool playerStateCheck) => PlayerAirborne = playerStateCheck;
    #endregion
}