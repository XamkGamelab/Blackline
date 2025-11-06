using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if (PlayerMovement.InputVector.magnitude == 0f) PlayerMovement.UpdateState(PlayerMovement.IdleState);

        if (!Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.RunKey)) PlayerMovement.UpdateState(PlayerMovement.WalkState);

        if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.SlideState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        HandleMove(PlayerMovement.PlayerData.RunSpeed);
    }
}
