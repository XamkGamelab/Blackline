public class CombatAndroidPatrolState : BaseEnemyPatrolState
{
    private CombatAndroid CombatAndroid => (CombatAndroid)Enemy;

    public CombatAndroidPatrolState(BaseEnemy enemy) : base(enemy) { }

    public override void HandleUpdate()
    {
        base.HandleUpdate();        
    }
}
