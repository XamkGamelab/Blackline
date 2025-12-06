public abstract class EnemyState<TEnemy> : BaseEnemyState where TEnemy : BaseEnemy
{
    // Finite State Machines are the fucking best. No they're not I hate you fuck you. -Shad //
    protected TEnemy Enemy { get; private set; }

    protected EnemyState(TEnemy enemy) => this.Enemy = enemy;
}
