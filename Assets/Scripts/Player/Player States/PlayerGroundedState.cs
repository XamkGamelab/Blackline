using UnityEngine;

public class PlayerGroundedState : PlayerMasterState
{
    public PlayerGroundedState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        PlayerMovement.BoostMoveVector = Vector3.zero;
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(SettingsHolder.Data.JumpKey)) PlayerMovement.UpdateState(PlayerMovement.JumpState); // MOTHERJUMPER //
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if(!PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.FallingState);
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
