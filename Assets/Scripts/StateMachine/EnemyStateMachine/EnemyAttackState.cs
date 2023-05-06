using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttackState : BaseState<EnemyCtrl>
{

    public float timeSkill = 0.25f;
    public float timeCountSkill = 0;

    private bool isThrowing;
    public void EnterState(EnemyCtrl enemy)
    {

        enemy.ChangeAnim(Constant.ANIM_ATTACK);
        isThrowing = false;
        enemy.agent.SetDestination(enemy.transform.position);
    }
    public void Update(EnemyCtrl enemy)
    {
        this.CountDownAttack(enemy);
        enemy.timeCountAttack += Time.deltaTime; 
        if (enemy.timeCountAttack >= enemy.timeLimitAttack)
        {
            this.timeCountSkill = 0;
            enemy.timeCountAttack = 0;
            enemy.ChangeState(enemy.move);
        }
    }
    private void CountDownAttack(EnemyCtrl enemy)
    {
        timeCountSkill += Time.deltaTime;
        if (timeCountSkill >= timeSkill && !isThrowing)
        {
            isThrowing = true;
            enemy.weaponImg.SetActive(false);
            enemy.SpawnWeapon();
        }
    }
}