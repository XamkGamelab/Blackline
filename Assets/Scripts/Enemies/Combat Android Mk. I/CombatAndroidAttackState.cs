using UnityEngine;

public class CombatAndroidAttackState : CombatAndroidConstantState
{
    public CombatAndroidAttackState(CombatAndroid enemy) : base(enemy) { }

    private float _attackDelayTimer, _attackEndTimer;
    private int _attackCounter;

    public override void Enter()
    {
        _attackDelayTimer = 0f;
        _attackEndTimer = 0f;
        _attackCounter = Random.Range(1, Enemy.DataSheet.AttackMaxAmount);

        Debug.Log("Attacking...");
    }

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

        if(_attackCounter > 0) HandleRevolver();

        if (_attackCounter == 0)
        {
            if(_attackEndTimer < Enemy.DataSheet.AttackEndDelay)
            {
                _attackEndTimer += Time.deltaTime;
                return;
            }

            Enemy.StateMachine.UpdateState(Enemy.RepositionState);
        }
    }

    private void HandleRevolver()
    {
        if (_attackDelayTimer < Enemy.DataSheet.AttackDelay)
        {
            _attackDelayTimer += Time.deltaTime;
            return;
        }

        if (Time.time > Enemy.NextShotTime)
        {
            Enemy.ShootRevolver();
            _attackCounter--;
        }
    }
}
