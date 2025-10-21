using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        base.Enter();

        if (Input.GetKey(SettingsHolder.Data.RunKey))
        {
            Debug.Log("Bunnyhop Boost!");
            PlayerMovement.BunnyhopVector.z = PlayerMovement.MoveVector.magnitude * PlayerMovement.PlayerData.BunnyHopSpeedMultiplier;
        }
        
        PlayerMovement.GravityVector.y = Mathf.Sqrt(PlayerMovement.PlayerData.JumpHeight * -2f * PlayerMovement.PlayerData.Gravity);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerMovement.GravityVector.y < 0f) PlayerMovement.UpdateState(PlayerMovement.FallingState);
    }
}
