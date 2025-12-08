using UnityEngine;
using UnityEngine.AI;

public class CombatAndroid : BaseEnemy
{
    [Header("Combat Android")]
    [SerializeField]
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;

    public CombatAndroidDataSheet DataSheet => (CombatAndroidDataSheet)BaseEnemyDataSheet; 

    public CombatAndroidPatrolState PatrolState;
    public CombatAndroidAttackState AttackState;

    private void Awake() => Initialize();

    public override void Initialize()
    {
        base.Initialize();

        PatrolState = new CombatAndroidPatrolState(this);
        IdleState = new CombatAndroidIdleState(this);
        AttackState = new CombatAndroidAttackState(this);

        StateMachine.UpdateState(IdleState);
    }

    public override void HandleStates()
    {
        base.HandleStates();
    }
}
