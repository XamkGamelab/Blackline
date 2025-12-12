using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerMovement movement) : base(movement) { }

    public override void HandleUpdate()
    {
        HandleMove();
        HandleGravity();
        HandleBunnyhop();
        HandleSlide();
    }

    // Since in this state the player is already grounded, we will still add a constant gravity, to make the player stick to the ground. -Shad //
    protected void HandleGravity()
    {
        if (PlayerMovement.CharacterController.isGrounded)
        {
            PlayerMovement.GravityVector.y = PlayerMovement.PlayerData.StaticGravity;
        }
        else
        {
            PlayerMovement.GravityVector.y += PlayerMovement.CurrentGravitySpeed * Time.deltaTime;
        }
    }

    // This slowly fades the bunnyhop vector to zero. -Shad //
    protected void HandleBunnyhop()
    {
        PlayerMovement.BunnyhopVector = PlayerMovement.BunnyhopVector.magnitude * PlayerMovement.TempDirectionVector;

        if(PlayerMovement.BunnyhopVector.magnitude > 0f) PlayerMovement.BunnyhopVector = Vector3.SmoothDamp(PlayerMovement.BunnyhopVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.BunnyHopSmoothingTimeGrounded);        

        PlayerMovement.BunnyhopVector = Vector3.ClampMagnitude(PlayerMovement.BunnyhopVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.BunnyHopSpeedMultiplier);
    }

    // This slowly fades the slide vector to zero. -Shad //
    protected void HandleSlide()
    {
        PlayerMovement.SlideVector = PlayerMovement.SlideVector.magnitude * PlayerMovement.TempDirectionVector;

        if (PlayerMovement.SlideVector.magnitude > 0f) PlayerMovement.SlideVector = Vector3.SmoothDamp(PlayerMovement.SlideVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.SlideOutSmoothingTime);

        PlayerMovement.SlideVector = Vector3.ClampMagnitude(PlayerMovement.SlideVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.SlideSpeedMultiplier);
    }

    protected void HandleMove()
    {
        if (PlayerMovement.MoveVector.magnitude > 0f) PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.MovementSmoothingTimeGrounded);

        PlayerMovement.MoveVector = Vector3.ClampMagnitude(PlayerMovement.MoveVector, PlayerMovement.PlayerData.RunSpeed);
    }
}
