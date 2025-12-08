using UnityEngine;

public class CombatAndroidIdleState : BaseEnemyIdleState
{
    private CombatAndroid CombatAndroid => (CombatAndroid)Enemy;

    public CombatAndroidIdleState(BaseEnemy enemy) : base(enemy) { }

    private float _patrolTimer = 0f;

    public override void Enter()
    {
        Debug.Log($"{CombatAndroid.gameObject.name} entered Idle State.");

        _patrolTimer = 0f;
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerDistance() < CombatAndroid.DataSheet.EngageDistance)
        {
            CombatAndroid.StateMachine.UpdateState(CombatAndroid.ChaseState);
        }

        _patrolTimer += Time.deltaTime;

        if (_patrolTimer >= CombatAndroid.DataSheet.PatrolPositioningFrequency)
        {
            CombatAndroid.StateMachine.UpdateState(CombatAndroid.PatrolState);
        }
    }

    public float PlayerDistance()
    {
        return Vector3.Distance(CombatAndroid.transform.position, CombatAndroid.PlayerPosition.IPlayerTransform.position);
    }
}
