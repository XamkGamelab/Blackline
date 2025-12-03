using UnityEngine;

public class RaycastWeaponTacticalReloadState : WeaponState<RaycastWeapon>
{
    public RaycastWeaponTacticalReloadState(RaycastWeapon weapon) : base(weapon) { }

    private float _reloadTimer = 0f;

    public override void Enter()
    {
        Weapon.WeaponAudio.PlayOnce(Weapon.DataSheet.TacticalReloadSound);
    }

    public override void HandleUpdate()
    {
        if (_reloadTimer >= Weapon.DataSheet.TacticalReloadTime)
        {
            Weapon.ReloadFunction();
            Weapon.StateMachine.UpdateState(Weapon.IdleState);
        }
        
        _reloadTimer += Time.deltaTime;
    }

    public override void Exit()
    {
        _reloadTimer = 0f;
    }
}
