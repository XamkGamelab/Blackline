using UnityEngine;
using UnityEngine.AI;

public class CombatAndroidPatrolState : CombatAndroidConstantState
{
    public CombatAndroidPatrolState(CombatAndroid enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log($"{Enemy.gameObject.name} entered Patrol State.");

        Enemy.Agent.speed = Enemy.DataSheet.PatrolSpeed;

        Vector3 randomPoint;

        if(RandomPoint(out randomPoint)) Enemy.Agent.SetDestination(randomPoint);        
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerDistance() < Enemy.DataSheet.EngageDistance)
        {
            Enemy.StateMachine.UpdateState(Enemy.ChaseState);
        }

        if (Enemy.Agent.remainingDistance <= 0.05f)
        {
            Enemy.StateMachine.UpdateState(Enemy.IdleState);
        }       
    }

    private bool RandomPoint(out Vector3 resultPoint)
    {
        Vector3 randomPoint = Enemy.transform.position + Random.insideUnitSphere * Enemy.DataSheet.PatrolDistance;
        NavMeshHit navHit;
        
        if(NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            resultPoint = navHit.position;
            return true;
        }

        resultPoint = Vector3.zero;
        return false;
    }
}
