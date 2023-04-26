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
    [SerializeField] private bool isIdle;

    public float timeCheck = 0.5f;
    public float timeCountCheck = 0;

    public BaseState currentState;
    public NavMeshAgent agent;

    public override void OnInit()
    {
        base.OnInit();
        rangeAttack = 5;
        this.ChangeState(move);
    }

    void Update()
    {
        currentState.Update(this);
        if (Vector3.Distance(transform.position, agent.destination) <= 0.1)
        {
            this.ChangeState(idle);
            isIdle = true;
        }
        if (isIdle)
        {
            if (this.CheckEnemy())
            {
                this.ChangeState(attack);
            }
            if (timeCountCheck >= timeCheck)
            {
                this.ChangeState(move);
                timeCountCheck = 0;
                isIdle= false;
            }
        }
    }

    internal void ChangeState(BaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}