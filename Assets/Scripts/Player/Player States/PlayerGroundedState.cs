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

        PlayerMovement.BunnyhopVector = Vector3.Lerp(PlayerMovement.BunnyhopVector, Vector3.zero, PlayerMovement.PlayerData.BunnyHopSmoothingRateGrounded * Time.deltaTime);        

        PlayerMovement.BunnyhopVector = Vector3.ClampMagnitude(PlayerMovement.BunnyhopVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.BunnyHopSpeedMultiplier);
    }

    // This slowly fades the slide vector to zero. -Shad //
    protected void HandleSlide()
    {
        PlayerMovement.SlideVector = PlayerMovement.SlideVector.magnitude * PlayerMovement.TempDirectionVector;

        PlayerMovement.SlideVector = Vector3.Lerp(PlayerMovement.SlideVector, Vector3.zero, PlayerMovement.PlayerData.SlideSmoothingRate * Time.deltaTime);

        PlayerMovement.SlideVector = Vector3.ClampMagnitude(PlayerMovement.SlideVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.SlideSpeedMultiplier);
    }

    protected void HandleMove(float playerSpeed)
    {
        float horizontalInput = PlayerInput.GetAxis("Horizontal");
        float verticalInput = PlayerInput.GetAxis("Vertical");

        PlayerMovement.InputVector = new(horizontalInput, 0f, verticalInput);

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        PlayerMovement.InputVector.Normalize();

        Vector3 targetVector = PlayerMovement.transform.TransformDirection(PlayerMovement.InputVector);

        PlayerMovement.MoveVector = Vector3.Lerp(PlayerMovement.MoveVector, targetVector * playerSpeed, PlayerMovement.PlayerData.MovementSmoothingRateGrounded * Time.deltaTime);

        PlayerMovement.MoveVector = Vector3.ClampMagnitude(PlayerMovement.MoveVector, PlayerMovement.PlayerData.RunSpeed);
    }
}
