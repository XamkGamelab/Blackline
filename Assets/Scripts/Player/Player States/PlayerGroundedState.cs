using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        PlayerMovement.BoostMoveVector = Vector3.zero;
    }

    public override void HandleInput()
    {
        if (Input.GetKey(SettingsHolder.Data.JumpKey)) PlayerMovement.UpdateState(PlayerMovement.JumpState); // MOTHERJUMPER //
    }

    public override void HandleUpdate()
    {
        if(!PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.FallingState);

        HandleGravity();
        //HandleBoost();
    }

    protected void HandleGravity()
    {
        PlayerMovement.GravityVector.y = PlayerMovement.PlayerData.Gravity;
        PlayerMovement.CharacterController.Move(PlayerMovement.GravityVector * Time.deltaTime);
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

        PlayerMovement.MoveVector = new(horizontalInput, 0f, verticalInput);

        // Makes sure the vector magnitude is never greater than 1f. -Shad //
        PlayerMovement.MoveVector.Normalize();

        PlayerMovement.SmoothMoveVector = Vector3.SmoothDamp(PlayerMovement.SmoothMoveVector, PlayerMovement.MoveVector, ref _refVector, PlayerMovement.PlayerData.MovementSmoothingTime);

        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.SmoothMoveVector) * playerSpeed * Time.deltaTime);
    }
}
