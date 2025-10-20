using UnityEngine;

public abstract class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        PlayerMovement.TempDirectionVector = GetDirectionVector();
    }

    public override void Exit()
    {
        //PlayerMovement.BoostMoveVector = Vector3.zero;
    }

    public override void HandleUpdate()
    {
        if (PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.WalkState);

        HandleGravity();
        HandleBoost();
        HandleMove();        
    }

    protected void HandleGravity()
    {
        PlayerMovement.GravityVector.y += PlayerMovement.PlayerData.Gravity * Time.deltaTime;
    }

    protected void HandleBoost()
    {
        PlayerMovement.BoostMoveVector.z = Mathf.Clamp(PlayerMovement.BoostMoveVector.z, 0f, 100f);

        if (PlayerMovement.BoostMoveVector.z > 0f) PlayerMovement.BoostMoveVector.z -= PlayerMovement.PlayerData.MovementSmoothingTime * Time.deltaTime * 10f;

        PlayerMovement.BoostMoveVector = PlayerMovement.BoostMoveVector.magnitude * PlayerMovement.TempDirectionVector;
    }

    protected void HandleMove()
    {
        Vector3 refVector = Vector3.zero;

        PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, Vector3.zero, ref refVector, PlayerMovement.PlayerData.MovementSmoothingTime);

        PlayerMovement.MoveVector = PlayerMovement.MoveVector.magnitude * PlayerMovement.TempDirectionVector;
    }

    protected Vector3 GetDirectionVector()
    {
        return PlayerMovement.MoveVector.normalized;
    }
}
