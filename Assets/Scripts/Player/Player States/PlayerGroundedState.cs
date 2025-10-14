using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState (PlayerMovement controller) : base(controller) { }

    public override void HandleInput()
    {
        if (Input.GetKey(_playerController.Settings.Data.JumpKey)) _playerController.UpdateState(_playerController.JumpState);
    }
}
