using UnityEngine;

public class CombatAndroidIdleState : BaseEnemyIdleState
{
    private CombatAndroid CombatAndroid => (CombatAndroid)Enemy;

    public CombatAndroidIdleState(BaseEnemy enemy) : base(enemy) { }

    public override void HandleUpdate()
    {
        if(PlayerDistance() < CombatAndroid.DataSheet.EngageDistance)
        {
            CombatAndroid.StateMachine.UpdateState(CombatAndroid.AttackState);
        }
    }

    public float PlayerDistance()
    {
        return Vector3.Distance(CombatAndroid.transform.position, CombatAndroid.PlayerPosition.IPlayerTransform.position);
    }
}
