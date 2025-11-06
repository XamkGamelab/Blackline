using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if (PlayerMovement.InputVector.magnitude > 0f) PlayerMovement.UpdateState(PlayerMovement.WalkState);

        if (Input.GetKey(GlobalSettingsHolder.Instance.PlayerSettingsData.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.CrouchState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.MovementSmoothingTimeGrounded);
    }
}
