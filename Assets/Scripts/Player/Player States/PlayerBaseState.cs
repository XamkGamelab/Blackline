using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    protected PlayerController _playerController;

    public PlayerBaseState(PlayerController controller)
    {
        this._playerController = controller;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void HandleUpdate() { }
}
