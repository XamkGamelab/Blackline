using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState (PlayerMovement controller) : base(controller) { }

    public override void HandleInput()
    {
        if (Input.GetKey(SettingsHolder.Data.JumpKey)) PlayerMovement.UpdateState(PlayerMovement.JumpState); // MOTHERJUMPER //
    }

    public override void HandleUpdate()
    {
        if(!PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.FallingState);
    }

    private Vector3 _smoothMoveVector, _refVector;
    protected void HandleMove(float playerSpeed)
    {
        float horizontalInput = PlayerInput.GetAxis("Horizontal");
        float verticalInput = PlayerInput.GetAxis("Vertical");

        Vector3 inputVector = new(horizontalInput, 0f, verticalInput);

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        inputVector.Normalize();

        _smoothMoveVector = Vector3.SmoothDamp(_smoothMoveVector, inputVector, ref _refVector, PlayerMovement.PlayerData.MovementSmoothingTime);

        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(_smoothMoveVector) * playerSpeed * Time.deltaTime);

        PlayerMovement.Velocity.y = PlayerMovement.PlayerData.Gravity;

        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.Velocity) * Time.deltaTime);
    }
}
