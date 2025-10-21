using UnityEngine;

public abstract class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(SettingsHolder.Data.JumpKey)) PlayerMovement.UpdateState(PlayerMovement.JumpState); // MOTHERJUMPER. -Shad //
    }

    public override void HandleUpdate()
    {
        if(!PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.FallingState);

        HandleGravity();
        HandleBunnyhop();
        HandleSlide();
    }

    // Since in this state the player is already grounded, we will still add a constant gravity, to make the player stick to the ground. -Shad //
    protected void HandleGravity()
    {
        if(PlayerMovement.CharacterController.isGrounded) PlayerMovement.GravityVector.y = PlayerMovement.PlayerData.Gravity;
    }

    // This slowly fades the bunnyhop vector to zero. -Shad //
    protected void HandleBunnyhop()
    {
        PlayerMovement.BunnyhopVector = PlayerMovement.BunnyhopVector.magnitude * PlayerMovement.TempDirectionVector;

        if(PlayerMovement.BunnyhopVector.magnitude > 0f) PlayerMovement.BunnyhopVector = Vector3.SmoothDamp(PlayerMovement.BunnyhopVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.BunnyHopSmoothingRateGrounded);        

        PlayerMovement.BunnyhopVector = Vector3.ClampMagnitude(PlayerMovement.BunnyhopVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.BunnyHopSpeedMultiplier);
    }

    // This slowly fades the slide vector to zero. -Shad //
    protected void HandleSlide()
    {
        PlayerMovement.SlideVector = PlayerMovement.SlideVector.magnitude * PlayerMovement.TempDirectionVector;

        if (PlayerMovement.SlideVector.magnitude > 0f) PlayerMovement.SlideVector = Vector3.SmoothDamp(PlayerMovement.SlideVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.SlideSmoothingRate);

        PlayerMovement.SlideVector = Vector3.ClampMagnitude(PlayerMovement.SlideVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.SlideSpeedMultiplier);
    }

    protected void HandleMove(float playerSpeed)
    {
        float horizontalInput = PlayerInput.GetAxis("Horizontal");
        float verticalInput = PlayerInput.GetAxis("Vertical");

        PlayerMovement.InputVector = new(horizontalInput, 0f, verticalInput);

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        PlayerMovement.InputVector.Normalize();

        Vector3 targetVector = PlayerMovement.transform.TransformDirection(PlayerMovement.InputVector) * playerSpeed;

        if(PlayerMovement.InputVector.magnitude == 0f)
        {
            if (PlayerMovement.MoveVector.magnitude > 0f) PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.MovementSmoothingRateGrounded);
        }
        else
        {
            PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, targetVector, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.MovementSmoothingRateGrounded);
        }

        PlayerMovement.MoveVector = Vector3.ClampMagnitude(PlayerMovement.MoveVector, PlayerMovement.PlayerData.RunSpeed);
    }
}
