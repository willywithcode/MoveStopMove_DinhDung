using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttackState : BaseState<EnemyCtrl>
{
    public void EnterState(EnemyCtrl enemy)
    {

        enemy.ChangeAnim(Constant.ANIM_ATTACK);
        enemy.agent.SetDestination(enemy.transform.position);
        enemy.isAttack = true;
    }
    public void Update(EnemyCtrl enemy)
    {
        enemy.Attack();
        enemy.timeCountAttack += Time.deltaTime; 
        if (enemy.timeCountAttack >= enemy.timeLimitAttack || !enemy.isAttack)
        {
            enemy.timeCountSkill = 0;
            enemy.timeCountAttack = 0;
            enemy.ChangeState(enemy.move);
        }
    }
}