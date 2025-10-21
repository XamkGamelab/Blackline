using UnityEngine;

public class PlayerCrouchState : PlayerGroundedState
{
    public PlayerCrouchState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        base.Enter();

        if (Input.GetKey(SettingsHolder.Data.RunKey))
        {
            Debug.Log("Slide Boost!");

            PlayerMovement.TempDirectionVector = GetDirectionVector();

            PlayerMovement.SlideVector.z = PlayerMovement.MoveVector.magnitude * PlayerMovement.PlayerData.SlideSpeedMultiplier;
        }

        PlayerMovement.SetControllerSize(true);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!Input.GetKey(SettingsHolder.Data.CrouchKey)) PlayerMovement.UpdateState(PlayerMovement.WalkState);
    }

    protected void HandleSlide()
    {
        PlayerMovement.SlideVector = PlayerMovement.SlideVector.magnitude * PlayerMovement.TempDirectionVector;

        PlayerMovement.SlideVector = Vector3.Lerp(PlayerMovement.SlideVector, Vector3.zero, PlayerMovement.PlayerData.SlideSmoothingRate * Time.deltaTime);

        PlayerMovement.SlideVector = Vector3.ClampMagnitude(PlayerMovement.SlideVector, PlayerMovement.PlayerData.RunSpeed * PlayerMovement.PlayerData.SlideSpeedMultiplier);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        HandleSlide();
        HandleMove(PlayerMovement.PlayerData.CrouchSpeed);
    }

    public override void Exit()
    {
        PlayerMovement.SetControllerSize(false);
    }
}
