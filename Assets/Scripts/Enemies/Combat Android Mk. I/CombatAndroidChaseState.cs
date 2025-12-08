public class CombatAndroidChaseState : CombatAndroidConstantState
{
    public CombatAndroidChaseState(CombatAndroid enemy) : base(enemy) { }

    public override void Enter()
    {
        Enemy.Agent.speed = Enemy.DataSheet.WalkSpeed;  
        Enemy.Agent.stoppingDistance = Enemy.DataSheet.AttackDistance;
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        Enemy.Agent.SetDestination(Enemy.PlayerPosition.IPlayerTransform.position);

        if (PlayerDistance() <= Enemy.DataSheet.AttackDistance)
        {
            Enemy.StateMachine.UpdateState(Enemy.AttackState);
        }
    }
}
