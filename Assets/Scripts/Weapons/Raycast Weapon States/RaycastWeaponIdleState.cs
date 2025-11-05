using UnityEngine;

public class RaycastWeaponIdleState : BaseWeaponIdleState
{
    private RaycastWeapon _raycastWeapon => (RaycastWeapon)Weapon;

    public RaycastWeaponIdleState(BaseWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Debug.Log("Idle!");
    }

    // Switch case loop for the different firing modes. Not very scalable, //
    // but fuck it. -Shad //
    public override void HandleInput()
    {
        base.HandleInput();

        switch (_raycastWeapon.CurrentFiringMode)
        {
            case FiringMode.Single:
                if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey) && _raycastWeapon.AmmoLeftInWeapon()) 
                {
                    Weapon.StateMachine.UpdateState(_raycastWeapon.FiringState);
                }
                break;
            case FiringMode.Automatic:
                if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey) && _raycastWeapon.AmmoLeftInWeapon()) 
                {
                    Weapon.StateMachine.UpdateState(_raycastWeapon.FiringState);
                }
                break;
            case FiringMode.Burst:
                if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey) && _raycastWeapon.AmmoLeftInWeapon())
                {
                    Weapon.StateMachine.UpdateState(_raycastWeapon.FiringState);
                    _raycastWeapon.CalculateBurstCount();
                }
                break;
        }

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ReloadKey))
        {
            Weapon.StateMachine.UpdateState(_raycastWeapon.ReloadState);
        }
    }
}
