public class CombatAndroidDeadState : EnemyState<CombatAndroid>
{
    public CombatAndroidDeadState(CombatAndroid enemy) : base(enemy) { }

    public override void Enter()
    {
        Enemy.Agent.SetDestination(Enemy.transform.position);
        Enemy.WeaponObject.SetActive(false);
        Enemy.NavCollider.enabled = false;
    }
}
