using UnityEngine;

public abstract class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(SettingsHolder.Data.JumpKey)) PlayerMovement.UpdateState(PlayerMovement.JumpState); // MOTHERJUMPER //
    }

    public override void HandleUpdate()
    {
        if(!PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.FallingState);

        HandleGravity();
        HandleBoost();
    }

    protected void HandleGravity()
    {
        if(PlayerMovement.CharacterController.isGrounded) PlayerMovement.GravityVector.y = PlayerMovement.PlayerData.Gravity;
    }

    protected void HandleBoost()
    {
        PlayerMovement.BoostMoveVector.z = Mathf.Clamp(PlayerMovement.BoostMoveVector.z, 0f, 100f);

        if(PlayerMovement.BoostMoveVector.z > 0f) PlayerMovement.BoostMoveVector.z -= PlayerMovement.PlayerData.MovementSmoothingTime * Time.deltaTime * 10f;

        PlayerMovement.BoostMoveVector = PlayerMovement.BoostMoveVector.magnitude * PlayerMovement.TempDirectionVector;
    }

    protected void HandleMove(float playerSpeed)
    {
        float horizontalInput = PlayerInput.GetAxis("Horizontal");
        float verticalInput = PlayerInput.GetAxis("Vertical");

        PlayerMovement.InputVector = new(horizontalInput, 0f, verticalInput);

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        PlayerMovement.InputVector.Normalize();

        PlayerMovement.MoveVector = PlayerMovement.InputVector;

        PlayerMovement.MoveVector = PlayerMovement.transform.TransformDirection(PlayerMovement.MoveVector) * playerSpeed;
    }
}
