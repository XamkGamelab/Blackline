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
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        Vector3 playerPos = Enemy.PlayerPosition.IPlayerTransform.position;
        playerPos.y = Enemy.transform.position.y;
        Vector3 directionToPlayer = (playerPos - Enemy.RaycastCheckPos.position + new Vector3(0f, 1.5f, 0f));

        Enemy.transform.LookAt(playerPos, Vector3.up);

        if (Physics.Raycast(Enemy.RaycastCheckPos.position, directionToPlayer, out RaycastHit hit))
        {
            if (!hit.collider.TryGetComponent<IPlayerHealth>(out IPlayerHealth playerHealth))
            {
                Enemy.StateMachine.UpdateState(Enemy.RepositionState);
            }
        }

        Debug.DrawRay(
            Enemy.RaycastCheckPos.position,
            directionToPlayer * 30f,
            Color.red,
            15f
            );

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
