using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if(!Input.GetKey(SettingsHolder.Data.RunKey)) PlayerMovement.UpdateState(PlayerMovement.WalkState);
    }

    public override void HandleUpdate()
    {
        HandleMove(PlayerMovement.PlayerData.RunSpeed);
    }
}
