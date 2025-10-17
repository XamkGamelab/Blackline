using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        if (Input.GetKey(SettingsHolder.Data.JumpKey)) PlayerMovement.UpdateState(PlayerMovement.JumpState); // MOTHERJUMPER //
    }

    public override void HandleUpdate()
    {
        if(!PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.FallingState);

        HandleBoost();
    }

    protected void HandleBoost()
    {
        PlayerMovement.BoostMoveVector.z = Mathf.Clamp(PlayerMovement.BoostMoveVector.z, 0f, 100f);

        PlayerMovement.BoostMoveVector.z -= PlayerMovement.PlayerData.MovementSmoothingTime * Time.deltaTime;

        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.BoostMoveVector) * Time.deltaTime);
    }

    private Vector3 _refVector;
    protected void HandleMove(float playerSpeed)
    {
        float horizontalInput = PlayerInput.GetAxis("Horizontal");
        float verticalInput = PlayerInput.GetAxis("Vertical");

        Vector3 inputVector = new(horizontalInput, 0f, verticalInput);

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        inputVector.Normalize();

        PlayerMovement.SmoothMoveVector = Vector3.SmoothDamp(PlayerMovement.SmoothMoveVector, inputVector, ref _refVector, PlayerMovement.PlayerData.MovementSmoothingTime);

        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.SmoothMoveVector) * playerSpeed * Time.deltaTime);

        PlayerMovement.GravityVector.y = PlayerMovement.PlayerData.Gravity;        
        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.GravityVector) * Time.deltaTime);

        PlayerMovement.BoostMoveVector.z = 0f;
        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.BoostMoveVector) * Time.deltaTime);
    }
}
