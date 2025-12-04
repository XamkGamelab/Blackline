using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        base.Enter();

        PlayerMovement.CurrentGravitySpeed = PlayerMovement.PlayerData.JumpGravity;

        if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.RunKey))
        {
            PlayerMovement.BunnyhopVector = PlayerMovement.MoveVector.magnitude * PlayerMovement.PlayerData.BunnyHopSpeedMultiplier * PlayerMovement.TempDirectionVector;
        }
        
        PlayerMovement.GravityVector.y = Mathf.Sqrt(PlayerMovement.PlayerData.JumpHeight * -2f * PlayerMovement.PlayerData.StaticGravity);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerMovement.GravityVector.y < 0f) PlayerMovement.UpdateState(PlayerMovement.FallingState);
    }
}
