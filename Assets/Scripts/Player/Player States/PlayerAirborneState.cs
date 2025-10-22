using UnityEngine;

public abstract class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        PlayerMovement.TempDirectionVector = GetDirectionVector();
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

        if (PlayerMovement.BunnyhopVector.magnitude > 0f) PlayerMovement.BunnyhopVector = Vector3.SmoothDamp(PlayerMovement.BunnyhopVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.BunnyHopSmoothingTimeAirborne);

        PlayerMovement.BunnyhopVector = Vector3.ClampMagnitude(PlayerMovement.BunnyhopVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.BunnyHopSpeedMultiplier);
    }

    protected void FadeMoveVector()
    {
        PlayerMovement.MoveVector = PlayerMovement.MoveVector.magnitude * PlayerMovement.TempDirectionVector;

        if (PlayerMovement.MoveVector.magnitude > 0f) PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.MovementSmoothingTimeAirborne);
    }
}
