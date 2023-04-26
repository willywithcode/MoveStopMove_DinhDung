using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : Character
{
    internal IdleState idle = new IdleState();
    internal MoveState move = new MoveState();
    internal AttackState attack = new AttackState();
    internal DeadState dead = new DeadState();

    public float timeLimitAttack = 1f;
    public float timeCountAttack = 0;
    public float timeCheckWait = 0.5f;
    public float timeCountCheckWait = 0;

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
    }

    internal void ChangeState(BaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}