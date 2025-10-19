using UnityEngine;

public class PlayerAirborneState : PlayerMasterState
{
    public PlayerAirborneState (PlayerMovement movement) : base(movement) { }

    public override void Enter()
    {
        PlayerMovement.TempDirectionVector = PlayerMovement.transform.forward;
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerMovement.CharacterController.isGrounded) PlayerMovement.UpdateState(PlayerMovement.WalkState);

        HandleMove();
    }

    protected void HandleMove()
    {
        Vector3 refVector = Vector3.zero;

        PlayerMovement.MoveVector = Vector3.SmoothDamp(PlayerMovement.MoveVector, Vector3.zero, ref refVector, PlayerMovement.PlayerData.MovementSmoothingTime);

        PlayerMovement.MoveVector = PlayerMovement.MoveVector.magnitude * PlayerMovement.TempDirectionVector;
    }
}
