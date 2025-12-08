using UnityEngine;

public class CombatAndroidConstantState : EnemyState<CombatAndroid>
{
    public CombatAndroidConstantState(CombatAndroid enemy) : base(enemy) { }

    public override void HandleUpdate()
    {
        if (Enemy._currentHealth <= 0f) Enemy.StateMachine.UpdateState(Enemy.DeadState);
    }

    public float PlayerDistance()
    {
        return Vector3.Distance(Enemy.transform.position, Enemy.PlayerPosition.IPlayerTransform.position);
    }
}
