using UnityEngine;

public class RaycastWeaponFiringState : RaycastWeaponBaseState
{
    public RaycastWeaponFiringState(RaycastWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Debug.Log("Firing!");

        Weapon.PrimaryFunction();
    }

    public override void HandleUpdate()
    {
        if (Time.time < Weapon.NextShotTime) return;

        switch (Weapon.CurrentFiringMode)
        {
            case FiringMode.Single:
                Weapon.UpdateState(Weapon.IdleState);
                break;
            case FiringMode.Automatic:
                if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey) && Weapon.AmmoLeftInWeapon())
                {
                    Weapon.PrimaryFunction();
                }
                else
                {
                    Weapon.UpdateState(Weapon.IdleState);
                }
                break;
            case FiringMode.Burst:
                if (Weapon.BurstShotsRemaining > 0)
                {
                    Weapon.PrimaryFunction();
                }
                else
                {
                    Weapon.UpdateState(Weapon.IdleState);
                }
                break;
        }
    }
}
