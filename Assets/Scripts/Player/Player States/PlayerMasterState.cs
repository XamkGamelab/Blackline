using UnityEngine;

public class PlayerMasterState : PlayerBaseState
{
    public PlayerMasterState(PlayerMovement movement) : base(movement) { }

    public override void HandleUpdate()
    {
        HandleGravity();
        HandleBoost();
    }

    protected void HandleGravity()
    {
        if (PlayerMovement.CharacterController.isGrounded) PlayerMovement.GravityVector.y = PlayerMovement.PlayerData.Gravity * 0.5f;
        else PlayerMovement.GravityVector.y += PlayerMovement.PlayerData.Gravity * Time.deltaTime;
    }

    protected void HandleBoost()
    {
        PlayerMovement.BoostMoveVector.z = Mathf.Clamp(PlayerMovement.BoostMoveVector.z, 0f, 100f);

        PlayerMovement.BoostMoveVector.z -= PlayerMovement.PlayerData.MovementSmoothingTime * Time.deltaTime;

        PlayerMovement.BoostMoveVector = PlayerMovement.BoostMoveVector.z * PlayerMovement.TempDirectionVector;
    }
}
