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

    public float initSpeed;
    public float initAttackRange;

    public PlayerAttackState attack = new PlayerAttackState();
    public PlayerMoveState move = new PlayerMoveState();   
    public PlayerDeadState dead = new PlayerDeadState();
    public PlayerIdleState idle = new PlayerIdleState();
    public PlayerPauseState pause = new PlayerPauseState();
    public BaseState<PlayerCtrl> currentState;

    public void Awake()
    {
        OnInit();
    }
    public override void OnInit()
    {
        base.OnInit();
        point = 1;
        scaleGrowth = 1;
        defeatPoint = 1;
        TF.localScale = Vector3.one * scaleGrowth;
        this.ChangeState(idle);
        this.AssignWeapon();
        speedTempPant = rangeTempWeapon = rangeTempHat = 0;
        pantCurrent = pantType.material;
        collider = GetComponent<Collider>();
        rangeCtrl = attackRoundObject.GetComponent<CircleAttack>();
        initAttackRange = Constant.foudationAttackRange;
        initSpeed = Constant.foudationSpeed;
        rangeAttack = initAttackRange;
        speed = initSpeed;
    }
    private void Start()
    {
        this.UpdateSaveData();
    }

    void Update()
    {
        direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        currentState.Update(this);
    }
    
    public void ChangeState(BaseState<PlayerCtrl> nextState)
    {
        if(currentState != null) currentState.ExitState(this);
        if(currentState != nextState)
        {
            currentState = nextState;
            currentState.EnterState(this);
        }
    }
    public override void ChangeDeadState()
    {
        base.ChangeDeadState();
        this.ChangeState(dead);
    }
    public override void GrowthCharacter()
    {
        base.GrowthCharacter();
        rangeAttack = initAttackRange * scaleGrowth;
        speed = initSpeed * scaleGrowth;
    }
    public void UpdateSaveData()
    {
        if (SaveGameManager.Instance.currentHat != 0)
        {
            hatCurrent = Instantiate(EquipmentManager.Instance.hatDatas[SaveGameManager.Instance.currentHat - 1].weaponImg, hatContainer);
            rangeTempHat = EquipmentManager.Instance.hatDatas[SaveGameManager.Instance.currentHat - 1].attackRange;
        }
        if (SaveGameManager.Instance.currentShield != 0)
        {
            shieldCurrent = Instantiate(EquipmentManager.Instance.shieldDatas[SaveGameManager.Instance.currentShield - 1].weaponImg, shieldContainer);
        }
        if (SaveGameManager.Instance.currentPant != 0)
        {
            pantType.material = EquipmentManager.Instance.pantDatas[SaveGameManager.Instance.currentPant - 1].material;
            speedTempPant = EquipmentManager.Instance.pantDatas[SaveGameManager.Instance.currentPant - 1].speed;
        }
        if (SaveGameManager.Instance.currentWeapon != 0)
        {
            typeWeapon = EquipmentManager.Instance.weaponDatas[SaveGameManager.Instance.currentWeapon - 1].weaponType.poolType;
            rangeTempWeapon = EquipmentManager.Instance.weaponDatas[SaveGameManager.Instance.currentWeapon - 1].attackRange;
            this.AssignWeapon();
        }
        initSpeed = Constant.foudationSpeed + speedTempPant * 0.1f;
        initAttackRange = Constant.foudationAttackRange + (rangeTempHat + rangeTempWeapon) * 0.1f;
        speed = initSpeed;
        rangeAttack = initAttackRange;
    }
}