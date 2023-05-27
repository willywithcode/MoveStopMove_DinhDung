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
    internal EnemyPauseState pause = new EnemyPauseState();

    public BaseState<EnemyCtrl> currentState;
    public MissionWayPoint prefab;
    public Collider collider;

    public override void OnInit()
    {
        base.OnInit();
        this.SpawnNewWayPoint();
        this.ClearOldWeapon();
        this.RandomWeapon();
        this.AssignWeapon();
        rangeAttack = 5;
        this.ChangeState(move);
        collider= this.GetComponent<Collider>();
        collider.enabled = true;
    }

    void Update()
    {
        currentState.Update(this);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        if (LevelManager.Instance.enemyCurrent.Contains(this)) LevelManager.Instance.enemyCurrent.Remove(this);
        this.wayPoint.OnDespawn();
        this.wayPoint = null;
        this.nameUI.OnDespawn();
        this.nameUI = null;

    }
    public void ChangeState(BaseState<EnemyCtrl> newState)
    {
        if (currentState != null) currentState.ExitState(this);
        if (currentState != newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }
    }
}