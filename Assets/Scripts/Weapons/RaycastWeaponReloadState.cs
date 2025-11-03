using UnityEngine;

public class RaycastWeaponReloadState : RaycastWeaponBaseState
{
    public RaycastWeaponReloadState(RaycastWeapon weapon) : base(weapon) { }

    public override void Enter()
    {
        Debug.Log("Reload!");
    }

    public override void HandleInput()
    {
        // If player interrupts the
    }
}
