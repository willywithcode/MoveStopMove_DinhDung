using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Character
{
    [SerializeField] private FloatingJoystick joystick;

    private Vector3 direct;
    private bool isAttack;


    public override void OnInit()
    {
        base.OnInit();
        animator.SetBool("IsIdle", true);
        isAttack = false;
        rangeAttack = 5;
        speed = 5;
    }

    void Update()
    {
        if (isAttack)
        {
            this.Attack();
        }
        else
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsIdle", true);
        }
        direct = new Vector3(joystick.Horizontal,0,joystick.Vertical);


        if (Vector3.Distance(direct, Vector3.zero) >= 0.0001f)
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
            this.CheckEnemy();
        }
    }
    private void Attack()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position,rangeAttack,layerCharacter);
        if (enemies.Length > 1)
        {
            animator.SetBool("IsAttack", true);
            animator.SetBool("IsIdle", false);
            timeCount += Time.deltaTime;
            if (timeCount >= timeSkill) weapon.SetActive(false);
            if (timeCount >= timeLimitAttack)
            {
                isAttack = false;
                timeCount = 0;
            }
            if (Mathf.Abs(joystick.Horizontal) >= 0.01f || Mathf.Abs(joystick.Vertical) >= 0.01f)
            {
                isAttack = false;
                timeCount = 0;
            }
            //Debug.Log(enemies[0].gameObject.name);
            //Character character = enemies[0].GetComponent<Character>();
            target = Cache.GetScript(enemies[0]);
                positionTarget = target.transform.position;
            if (target != null)
            {
            }
        }
    }
    private void CheckEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack, layerCharacter);
        if (enemies.Length > 1)
        {
            isAttack = true;
        }
    }
}
