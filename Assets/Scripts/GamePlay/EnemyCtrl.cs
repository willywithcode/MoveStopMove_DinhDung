using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : Character
{
    internal EnemyIdleState idle = new EnemyIdleState();
    internal EnemyMoveState move = new EnemyMoveState();
    internal EnemyAttackState attack = new EnemyAttackState();
    internal EnemyDeadState dead = new EnemyDeadState();

    public float timeCheckWait = 0.5f;
    public float timeCountCheckWait = 0;
    public BaseState<EnemyCtrl> currentState;


    public override void OnInit()
    {
        base.OnInit();
        rangeAttack = 5;
        this.ChangeState(move);
        this.GetComponent<Collider>().enabled = true;
    }

    void Update()
    {
        currentState.Update(this);
    }

    public void ChangeState(BaseState<EnemyCtrl> newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}