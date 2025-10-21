using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState (PlayerMovement movement) : base(movement) { }

    public override void HandleInput()
    {
        base.HandleInput();

        if (Input.GetKey(SettingsHolder.Data.RunKey)) PlayerMovement.UpdateState(PlayerMovement.RunState);

        if (Input.GetKey(SettingsHolder.Data.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.CrouchState);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        HandleSlide();
        HandleMove(PlayerMovement.PlayerData.WalkSpeed);
    }

    protected void HandleSlide()
    {
        PlayerMovement.SlideVector = PlayerMovement.SlideVector.magnitude * PlayerMovement.TempDirectionVector;

        PlayerMovement.SlideVector = Vector3.Lerp(PlayerMovement.SlideVector, Vector3.zero, PlayerMovement.PlayerData.SlideSmoothingRate * Time.deltaTime);

        PlayerMovement.SlideVector = Vector3.ClampMagnitude(PlayerMovement.SlideVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.SlideSpeedMultiplier);
    }
}
