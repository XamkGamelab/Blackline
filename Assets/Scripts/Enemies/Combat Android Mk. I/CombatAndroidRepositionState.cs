using UnityEngine;
using UnityEngine.AI;

public class CombatAndroidRepositionState : CombatAndroidConstantState
{
    public CombatAndroidRepositionState(CombatAndroid enemy) : base(enemy) { }

    public override void Enter()
    {
        Enemy.Agent.speed = Enemy.DataSheet.WalkSpeed;
        Enemy.Agent.stoppingDistance = 0f;

        Enemy.NextRepositionTime += Enemy.DataSheet.RepositionCooldown;

        Enemy.Agent.SetDestination(RandomPoint());
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (Enemy.Agent.remainingDistance <= 0.1f)
        {
            Enemy.StateMachine.UpdateState(Enemy.AttackState);
        }       
    }

    private Vector3 RandomPoint()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized * Enemy.DataSheet.RepositionDistance;
        Vector3 randomPos = Enemy.transform.position + new Vector3(randomDir.x, 0f, randomDir.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, Enemy.Agent.height * 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // If somehow invalid, retry:
        return randomPos;
    }
}
