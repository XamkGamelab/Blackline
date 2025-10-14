using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    protected PlayerMovement _playerController;

    public PlayerBaseState(PlayerMovement controller)
    {
        this._playerController = controller;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void HandleUpdate() { }
}
