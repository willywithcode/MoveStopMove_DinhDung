using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
        this.ChangeState(idle);
        this.point = LevelManager.Instance.RandomPoint();
        this.SpawnNewWayPoint();
        this.ClearOldWeapon();
        this.RandomWeapon();
        this.AssignWeapon();
        rangeAttack = Constant.foudationAttackRange;
        agent.speed = Constant.foudationSpeed;
        this.GrowthCharacter();
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
        this.nameUI.OnDespawn();
        this.wayPoint.OnDespawn();
        this.wayPoint = null;
        this.nameUI = null;

    }
    public override void GrowthCharacter()
    {
        base.GrowthCharacter();
        rangeAttack = Constant.foudationAttackRange * scaleGrowth;
        this.agent.speed = Constant.foudationSpeed * scaleGrowth;
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
    public override void ChangeDeadState()
    {
        this.ChangeState(dead);
    }
}