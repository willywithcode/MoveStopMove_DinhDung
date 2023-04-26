using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
   public override void EnterState(EnemyCtrl enemy)
    {
        enemy.currentState= this;
        enemy.ChangeAnim(Constant.ANIM_IDLE);
    }
    public override void Update(EnemyCtrl enemy)
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
