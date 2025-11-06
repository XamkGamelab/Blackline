using UnityEngine;

public class RaycastWeaponReloadState : WeaponState<RaycastWeapon>
{
    public RaycastWeaponReloadState(RaycastWeapon weapon) : base(weapon) { }

    private float _reloadTimer = 0f;

    public override void Enter()
    {
        Weapon.WeaponAudio.PlayOnce(Weapon.DataSheet.EmergencyReloadSound);
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
