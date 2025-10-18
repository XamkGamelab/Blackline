using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        PlayerMovement.TempDirectionVector = PlayerMovement.transform.forward;
    }

    public override void HandleUpdate()
    {
        if (PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.WalkState);

        HandleGravity();
        HandleBoost();
    }

    protected void HandleGravity()
    {
        PlayerMovement.GravityVector.y += PlayerMovement.PlayerData.Gravity * Time.deltaTime;

        PlayerMovement.CharacterController.Move(PlayerMovement.GravityVector * Time.deltaTime);
    }

    protected void HandleBoost()
    {
        PlayerMovement.BoostMoveVector.z = Mathf.Clamp(PlayerMovement.BoostMoveVector.z, 0f, 100f);

        PlayerMovement.BoostMoveVector.z -= PlayerMovement.PlayerData.MovementSmoothingTime * Time.deltaTime;

        PlayerMovement.CharacterController.Move(PlayerMovement.BoostMoveVector.z * Time.deltaTime * PlayerMovement.TempDirectionVector);
    }
}
