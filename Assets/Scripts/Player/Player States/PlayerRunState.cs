using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!Input.GetKey(SettingsHolder.Data.RunKey)) PlayerMovement.UpdateState(PlayerMovement.WalkState);

        if (Input.GetKey(SettingsHolder.Data.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.SlideState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        HandleMove(PlayerMovement.PlayerData.RunSpeed);
    }
}
