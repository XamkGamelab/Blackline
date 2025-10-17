using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetKey(SettingsHolder.Data.RunKey)) PlayerMovement.UpdateState(PlayerMovement.RunState);
    }

    public override void HandleUpdate()
    {
        HandleMove(PlayerMovement.PlayerData.WalkSpeed);
    }
}
