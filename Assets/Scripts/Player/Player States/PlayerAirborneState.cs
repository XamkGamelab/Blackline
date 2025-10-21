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
        HandleBunnyhop();
        FadeMoveVector();        
    }

    protected void HandleGravity()
    {
        PlayerMovement.GravityVector.y += PlayerMovement.PlayerData.Gravity * Time.deltaTime;
    }

    protected void HandleBunnyhop()
    {
        PlayerMovement.BunnyhopVector = PlayerMovement.BunnyhopVector.magnitude * PlayerMovement.TempDirectionVector;

        PlayerMovement.BunnyhopVector = Vector3.Lerp(PlayerMovement.BunnyhopVector, Vector3.zero, PlayerMovement.PlayerData.BunnyHopSmoothingRateAirborne * Time.deltaTime);

        PlayerMovement.BunnyhopVector = Vector3.ClampMagnitude(PlayerMovement.BunnyhopVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.BunnyHopSpeedMultiplier);
    }

    protected void FadeMoveVector()
    {
        PlayerMovement.MoveVector = Vector3.Lerp(PlayerMovement.MoveVector, Vector3.zero, PlayerMovement.PlayerData.MovementSmoothingRateAirborne * Time.deltaTime);

        PlayerMovement.MoveVector = PlayerMovement.MoveVector.magnitude * PlayerMovement.TempDirectionVector;
    }
}
