using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrl : Character
{
    [SerializeField] private FloatingJoystick joystick;

    private Vector3 direct;
    private bool isAttack;
    void Start()
    {
        animator.SetBool("IsIdle", true);
        isAttack  = false;
        speed = 5;
    }

    void Update()
    {
        if (isAttack)
        {
            animator.SetBool("IsAttack", true);
            animator.SetBool("IsIdle", false);
            timeCount += Time.deltaTime;
            if (timeCount >= timeLimitAttack )
            {
                isAttack = false;
                timeCount = 0;
            }
            if (Mathf.Abs( joystick.Horizontal) >=  0.01f || Mathf.Abs( joystick.Vertical) >= 0.01f)
            {
                isAttack = false;
                timeCount = 0;
            }
        }
        else
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsIdle", true);
        }
        direct = new Vector3(joystick.Horizontal,0,joystick.Vertical);


        if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f)
        {
            transform.position += direct * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direct);
            animator.SetBool("IsUlti", true);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsUlti", false);
            animator.SetBool("IsIdle", true);  
            if (Input.GetMouseButtonDown(0))
            {
                isAttack= true;
            }
        }
    }
}
