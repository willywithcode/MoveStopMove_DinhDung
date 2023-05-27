using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCtrl : Character
{
    public FloatingJoystick joystick;
    public Vector3 direct;
    public Collider collider;
    public GameObject attackRoundObject;
    public CircleAttack rangeCtrl;

    public GameObject hatCurrent;
    public GameObject shieldCurrent;
    public Material pantCurrent;

    public float speedTempPant;
    public float rangeTempWeapon;
    public float rangeTempHat;
   

    public PlayerAttackState attack = new PlayerAttackState();
    public PlayerMoveState move = new PlayerMoveState();   
    public PlayerDeadState dead = new PlayerDeadState();
    public PlayerIdleState idle = new PlayerIdleState();
    public PlayerPauseState pause = new PlayerPauseState();
    public BaseState<PlayerCtrl> currentState;
    public override void OnInit()
    {
        base.OnInit();
        //this.SpawnNewWayPoint();
        this.ChangeState(idle);
        this.AssignWeapon();
        speedTempPant = rangeTempWeapon = rangeTempHat = 0;
        pantCurrent = pantType.material;
        collider = GetComponent<Collider>();
        rangeCtrl = attackRoundObject.GetComponent<CircleAttack>();
        rangeAttack = 5;
        speed = 5;
    }

    void Update()
    {
        direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        currentState.Update(this);
    }
    
    public void ChangeState(BaseState<PlayerCtrl> nextState)
    {
        if (currentState != null) currentState.ExitState(this);
        if(currentState != nextState)
        {
            currentState = nextState;
            currentState.EnterState(this);
        }
    }
}