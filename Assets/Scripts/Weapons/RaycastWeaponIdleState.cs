using UnityEngine;

public class RaycastWeaponIdleState : RaycastWeaponBaseState
{
    public RaycastWeaponIdleState(RaycastWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Debug.Log("Idle!");
    }

    // Switch case loop for the different firing modes. Not very scalable, //
    // but fuck it. -Shad //
    public override void HandleInput()
    {
        switch (Weapon.CurrentFiringMode)
        {
            case FiringMode.Single:
                if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey)) 
                {
                    Weapon.UpdateState(Weapon.FiringState);
                }
                break;
            case FiringMode.Automatic:
                if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey)) 
                {
                    Weapon.UpdateState(Weapon.FiringState);
                }
                break;
            case FiringMode.Burst:
                if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
                {
                    Weapon.UpdateState(Weapon.FiringState);
                    Weapon.CalculateBurstCount();
                }
                break;
        }

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ReloadKey))
        {
            Weapon.UpdateState(Weapon.ReloadState);
        }
    }
}
