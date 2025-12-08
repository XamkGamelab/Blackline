public class EnemyStateMachine<TEnemy> where TEnemy : BaseEnemy
{
    public BaseEnemyState CurrentState { get; private set; }

    public void UpdateState(BaseEnemyState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void HandleStates()
    {
        CurrentState.HandleUpdate();
    }
}