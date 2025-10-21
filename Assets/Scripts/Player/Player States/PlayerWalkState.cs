using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetKey(SettingsHolder.Data.RunKey)) PlayerMovement.UpdateState(PlayerMovement.RunState);

        if (Input.GetKey(SettingsHolder.Data.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.CrouchState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        HandleMove(PlayerMovement.PlayerData.WalkSpeed);
    }
}
