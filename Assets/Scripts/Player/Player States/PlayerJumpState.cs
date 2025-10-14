using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState (PlayerMovement controller) : base(controller) { }

    public override void Enter()
    {
        PlayerMovement.Velocity.y = Mathf.Sqrt(PlayerMovement.PlayerData.JumpHeight * -2f * PlayerMovement.PlayerData.Gravity);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerMovement.Velocity.y < 0f) PlayerMovement.UpdateState(PlayerMovement.FallingState);
    }
}
