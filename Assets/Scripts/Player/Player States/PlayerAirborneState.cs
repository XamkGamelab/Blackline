using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState (PlayerMovement movement) : base(movement) { }

    public override void HandleUpdate()
    {
        if (PlayerMovement.CharacterController.isGrounded)
        {
            if(Input.GetKey(SettingsHolder.Data.RunKey)) PlayerMovement.UpdateState(PlayerMovement.RunState);
            else PlayerMovement.UpdateState(PlayerMovement.WalkState);
        }

        HandleGravity();
        HandleBoost();
    }

    protected void HandleGravity()
    {
        PlayerMovement.GravityVector.y += PlayerMovement.PlayerData.Gravity * Time.deltaTime;
        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.GravityVector) * Time.deltaTime);
    }

    protected void HandleBoost()
    {
        PlayerMovement.BoostMoveVector.z = Mathf.Clamp(PlayerMovement.BoostMoveVector.z, 0f, 100f);

        PlayerMovement.BoostMoveVector.z -= PlayerMovement.PlayerData.MovementSmoothingTime * Time.deltaTime;

        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.BoostMoveVector) * Time.deltaTime);
    }

    private Vector3 _refVector;
    protected void ReduceHorizontalSpeed()
    {
        PlayerMovement.SmoothMoveVector = Vector3.SmoothDamp(PlayerMovement.SmoothMoveVector, Vector3.zero, ref _refVector, PlayerMovement.PlayerData.MovementSmoothingTime);

        PlayerMovement.CharacterController.Move(PlayerMovement.transform.TransformDirection(PlayerMovement.SmoothMoveVector) * Time.deltaTime);
    }
}
