using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    protected PlayerController _controller;

    public PlayerBaseState(PlayerController controller)
    {
        this._controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void HandleUpdate() { }
}
