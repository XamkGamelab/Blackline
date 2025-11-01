using UnityEngine;

public class RaycastWeaponIdleState : RaycastWeaponBaseState
{
    public RaycastWeaponIdleState(RaycastWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Debug.Log("Idle!");
    }

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
    }
}
