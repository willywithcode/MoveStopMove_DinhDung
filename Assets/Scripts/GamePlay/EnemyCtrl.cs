using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : Character
{
    private IdleState idle = new IdleState();
    private MoveState move = new MoveState();
    private AttackState attack = new AttackState();
    private DeadState dead = new DeadState();
    private float timeCheck = 0.2f;
    private float timeCountCheck = 0;

    public BaseState currentState;
    public NavMeshAgent agent;

    public override void OnInit()
    {
        base.OnInit();
        this.ChangeState(move);
        rangeAttack = 5;
    }

    void Update()
    {
        currentState.Update(this);
        timeCountCheck += Time.deltaTime;
        if (timeCountCheck >= timeCheck)
        {
            this.CheckEnemy();
            timeCountCheck = 0;
        }
    }

    private void ChangeState(BaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    private void CheckEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack, layerCharacter);
        if (enemies.Length > 1)
        {
            ChangeState(idle);
            ChangeState(attack);
        }
    }
}
