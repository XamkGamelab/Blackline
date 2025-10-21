using UnityEngine;

public class PlayerSlideState : PlayerGroundedState
{
    public PlayerSlideState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        PlayerMovement.SetControllerSize(true);

        PlayerMovement.TempDirectionVector = GetDirectionVector();

        PlayerMovement.SlideVector.z = PlayerMovement.MoveVector.magnitude * PlayerMovement.PlayerData.SlideSpeedMultiplier;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!Input.GetKey(SettingsHolder.Data.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.WalkState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerMovement.SlideVector.magnitude < 1f) PlayerMovement.UpdateState(PlayerMovement.CrouchState);

        HandleSlide();
        FadeMoveVector();
    }

    protected void HandleSlide()
    {
        PlayerMovement.SlideVector = PlayerMovement.SlideVector.magnitude * PlayerMovement.TempDirectionVector;

        PlayerMovement.SlideVector = Vector3.Lerp(PlayerMovement.SlideVector, Vector3.zero, PlayerMovement.PlayerData.SlideSmoothingRate * Time.deltaTime);

        PlayerMovement.SlideVector = Vector3.ClampMagnitude(PlayerMovement.SlideVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.SlideSpeedMultiplier);
    }

    protected void FadeMoveVector()
    {
        PlayerMovement.MoveVector = Vector3.Lerp(PlayerMovement.MoveVector, Vector3.zero, PlayerMovement.PlayerData.MovementSmoothingRateAirborne * Time.deltaTime);

        PlayerMovement.MoveVector = PlayerMovement.MoveVector.magnitude * PlayerMovement.TempDirectionVector;
    }

    public override void Exit()
    {
        PlayerMovement.SetControllerSize(false);
    }
}
