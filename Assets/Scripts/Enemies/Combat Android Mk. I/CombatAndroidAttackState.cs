using UnityEngine;

public class CombatAndroidAttackState : CombatAndroidConstantState
{
    public CombatAndroidAttackState(CombatAndroid enemy) : base(enemy) { }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        Vector3 target = Enemy.PlayerPosition.IPlayerTransform.position;
        target.y = Enemy.transform.position.y;        

        Enemy.transform.LookAt(target, Vector3.up);

        if (PlayerDistance() > Enemy.DataSheet.AttackDistance)
        {
            Enemy.StateMachine.UpdateState(Enemy.ChaseState);
        }

        HandleRevolver();
    }

    private void HandleRevolver()
    {
        if (Time.time > Enemy.NextShotTime) Enemy.ShootRevolver();
    }
}
