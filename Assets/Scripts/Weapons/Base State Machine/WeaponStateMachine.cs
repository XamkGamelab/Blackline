public class WeaponStateMachine<TWeapon> where TWeapon : BaseWeapon
{
    public BaseWeaponState CurrentState { get; private set; }

    public void UpdateState(BaseWeaponState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void HandleStates()
    {
        CurrentState.HandleInput();
        CurrentState.HandleUpdate();
    }
}