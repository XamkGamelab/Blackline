using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerMovement PlayerMovement;

    public PlayerBaseState(PlayerMovement playerMovement)
    {
        this.PlayerMovement = playerMovement;        
    }

    // This is the base, the foundation for all the player states. Every state will inherit this class, with these methods. -Shad //

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void HandleUpdate() { }

    protected Vector3 GetDirectionVector()
    {
        return PlayerMovement.MoveVector.normalized;
    }
}
