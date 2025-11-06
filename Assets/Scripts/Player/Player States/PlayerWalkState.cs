using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if (PlayerMovement.InputVector.magnitude == 0f) PlayerMovement.UpdateState(PlayerMovement.IdleState);

        if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.RunKey)) PlayerMovement.UpdateState(PlayerMovement.RunState);

        if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.CrouchState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        HandleMove(PlayerMovement.PlayerData.WalkSpeed);
    }
}
