using UnityEngine;

public class PlayerSlideState : PlayerGroundedState
{
    public PlayerSlideState (PlayerMovement movement) : base(movement) { }

    // Entering Slide, this resizes the CharacterController to a smaller size and adds a small boost. -Shad //
    public override void Enter()
    {
        PlayerMovement.SetControllerSize(true);

        Debug.Log("Slide Boost!");

        PlayerMovement.TempDirectionVector = GetDirectionVector();

        PlayerMovement.SlideVector.z = PlayerMovement.MoveVector.magnitude * PlayerMovement.PlayerData.SlideSpeedMultiplier;
    }

    public override void HandleInput()
    {
        if (!Input.GetKey(SettingsHolder.Data.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.WalkState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerMovement.SlideVector.magnitude < 1f) PlayerMovement.UpdateState(PlayerMovement.CrouchState);

        FadeMoveVector();
    }

    // This slowly fades the normal movement vector to zero. -Shad //
    protected void FadeMoveVector()
    {
        PlayerMovement.MoveVector = PlayerMovement.MoveVector.magnitude * PlayerMovement.TempDirectionVector;

        if (PlayerMovement.MoveVector.magnitude > 0f) PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, Vector3.zero, ref PlayerMovement.RefVector, PlayerMovement.PlayerData.MovementSmoothingRateAirborne);
    }

    // Exiting Slide, this resizes the CharacterController to its normal size. -Shad //
    public override void Exit()
    {
        PlayerMovement.SetControllerSize(false);
    }
}
