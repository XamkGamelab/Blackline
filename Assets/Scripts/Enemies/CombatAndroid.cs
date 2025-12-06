using UnityEngine;
using UnityEngine.AI;

public class CombatAndroid : BaseEnemy
{
    [Header("Combat Android")]
    [SerializeField]
    private NavMeshAgent _agent;

    public CombatAndroidPatrolState PatrolState;

    private void Awake() => Initialize();

    public override void Initialize()
    {
        base.Initialize();

        PatrolState = new CombatAndroidPatrolState(this);
        IdleState = new CombatAndroidIdleState(this);

        StateMachine.UpdateState(IdleState);
    }

    public override void HandleStates()
    {
        base.HandleStates();
    }
}
