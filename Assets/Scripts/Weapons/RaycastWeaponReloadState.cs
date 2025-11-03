using UnityEngine;

public class RaycastWeaponReloadState : RaycastWeaponBaseState
{
    public RaycastWeaponReloadState(RaycastWeapon weapon) : base(weapon) { }

    private float _reloadTimer;

    public override void Enter()
    {
        Debug.Log("Reload!");
    }

    public override void HandleInput()
    {
        // Player should be able to interrupt reload by switching guns. -Shad //
    }

    public override void HandleUpdate()
    {
        // Enter reload timer here please. -Shad //
    }
}
