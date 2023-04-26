using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Character
{
    [SerializeField] private FloatingJoystick joystick;

    private Vector3 direct;
    private bool isRun;


    public override void OnInit()
    {
        base.OnInit();
        isRun = false;
        rangeAttack = 5;
        speed = 5;
    }

    void Update()
    {
        
        direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (!isRun)
        {
            if (this.CheckEnemy())
            {
                this.Attack();
            } 
            else
            {
                ChangeAnim(Constant.ANIM_IDLE);
            }
            if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f) isRun = true;
        }
        else
        {
            ChangeAnim(Constant.ANIM_RUN);
            transform.position += direct * speed * Time.deltaTime;
            float angle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg;
            if (angle != 0) transform.rotation = Quaternion.Euler(0f, angle, 0f);
            if (Vector3.Distance(direct, Vector3.zero) <= 0.00001f) isRun = false;
        }

    }
    
    
}