using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttackState : BaseState<EnemyCtrl>
{

    private float timeSkill = 0.25f;
    private float timeCount = 0;
    private float timeLimitAttack = 0.85f;

    private bool isThrowing;
    public void EnterState(EnemyCtrl enemy)
    {

        enemy.ChangeAnim(Constant.ANIM_ATTACK);
        isThrowing = false;
        enemy.agent.SetDestination(enemy.transform.position);
    }
    public void Update(EnemyCtrl enemy)
    {
        timeCount += Time.deltaTime; 
        this.CountDownAttack(enemy);
        if (timeCount >= timeLimitAttack)
        {
            enemy.ChangeState(enemy.move);
        }
    }
    private void CountDownAttack(EnemyCtrl enemy)
    {
        if (timeCount >= timeSkill && !isThrowing)
        {
            isThrowing = true;
            enemy.Rotate();
            enemy.weaponImg.SetActive(false);
            WeaponManager.Instance.SpawnWeapon(enemy);
        }
    }
}