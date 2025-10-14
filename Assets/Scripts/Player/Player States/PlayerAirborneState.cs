using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState (PlayerMovement controller) : base(controller) { }

    public override void HandleUpdate()
    {
        if (PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.WalkState);

        HandleGravity();
    }

    protected void HandleGravity()
    {
        PlayerMovement.Velocity.y += PlayerMovement.PlayerData.Gravity * Time.deltaTime;
        PlayerMovement.CharacterController.Move(PlayerMovement.Velocity * Time.deltaTime);
    }
}
