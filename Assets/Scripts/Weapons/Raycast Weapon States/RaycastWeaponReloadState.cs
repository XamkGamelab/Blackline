using UnityEngine;

public class RaycastWeaponReloadState : WeaponState<RaycastWeapon>
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
        if (_reloadTimer >= Weapon.DataSheet.EmergencyReloadTime)
        {
            Weapon.ReloadWeapon();
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
        }
        
        _reloadTimer += Time.deltaTime;
    }

    public override void Exit()
    {
        _reloadTimer = 0f;
    }
}
