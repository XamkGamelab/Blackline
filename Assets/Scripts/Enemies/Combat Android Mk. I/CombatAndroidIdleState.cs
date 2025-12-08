using UnityEngine;

public class CombatAndroidIdleState : BaseEnemyIdleState
{
    private CombatAndroid CombatAndroid => (CombatAndroid)Enemy;

    public CombatAndroidIdleState(BaseEnemy enemy) : base(enemy) { }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (PlayerDistance() < CombatAndroid.DataSheet.EngageDistance)
        {
            CombatAndroid.StateMachine.UpdateState(CombatAndroid.ChaseState);
        }
    }

    public float PlayerDistance()
    {
        return Vector3.Distance(CombatAndroid.transform.position, CombatAndroid.PlayerPosition.IPlayerTransform.position);
    }
}
