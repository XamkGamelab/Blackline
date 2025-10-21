using UnityEngine;

public class PlayerCrouchState : PlayerGroundedState
{
    public PlayerCrouchState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {       
        PlayerMovement.SetControllerSize(true);
    }

    public override void HandleInput()
    {
        if (!Input.GetKey(SettingsHolder.Data.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.WalkState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        HandleMove(PlayerMovement.PlayerData.CrouchSpeed);
    }

    // Exiting Slide, this resizes the CharacterController to its normal size. -Shad //
    public override void Exit()
    {
        PlayerMovement.SetControllerSize(false);
    }
}
