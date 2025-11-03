using UnityEngine;

public class RaycastWeaponReloadState : RaycastWeaponBaseState
{
    public RaycastWeaponReloadState(RaycastWeapon weapon) : base(weapon) { }

    private float _reloadTimer;

    public override void Enter()
    {
        Debug.Log("Reload!");

        _reloadTimer = 0f;
    }

    public override void HandleInput()
    {
        // Player should be able to interrupt reload by switching guns. -Shad //
    }

    public override void HandleUpdate()
    {
        if (_reloadTimer >= Weapon.DataSheet.ReloadTime)
        {
            Weapon.ReloadWeapon();
            Weapon.UpdateState(Weapon.IdleState);
        }
        
        _reloadTimer += Time.deltaTime;
    }
}
