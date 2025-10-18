using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        base.Enter();

        if (Input.GetKey(SettingsHolder.Data.RunKey)) PlayerMovement.BoostMoveVector.z = PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.RunJumpSpeedMultiplier;
        
        PlayerMovement.GravityVector.y = Mathf.Sqrt(PlayerMovement.PlayerData.JumpHeight * -2f * PlayerMovement.PlayerData.Gravity);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerMovement.GravityVector.y < 0f) PlayerMovement.UpdateState(PlayerMovement.FallingState);
    }
}
