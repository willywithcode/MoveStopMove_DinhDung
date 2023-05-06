using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : BaseState<EnemyCtrl>
{
   public void EnterState(EnemyCtrl enemy)
    {
        enemy.ChangeAnim(Constant.ANIM_IDLE);
    }
    public void Update(EnemyCtrl enemy)
    {
        enemy.timeCountCheckWait += Time.deltaTime;
        if (enemy.CheckEnemy())
        {
            enemy.ChangeState(enemy.attack);
        }
        if (enemy.timeCountCheckWait >= enemy.timeCheckWait)
        {
            enemy.ChangeState(enemy.move);
            enemy.timeCountCheckWait = 0;
        }
    }
}
