using UnityEngine;

public class MeleeWeaponIdleState : BaseWeaponIdleState
{
    private MeleeWeapon _meleeWeapon => (MeleeWeapon)Weapon;

    public MeleeWeaponIdleState(BaseWeapon weapon) : base(weapon) { }

    // Switch case loop for the different firing modes. Not very scalable, //
    // but fuck it. -Shad //
    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
        {
            Weapon.StateMachine.UpdateState(_meleeWeapon.LeftSwingState);
        }
    }
}
