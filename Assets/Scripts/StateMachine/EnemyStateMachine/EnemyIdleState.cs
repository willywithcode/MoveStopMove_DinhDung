using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : BaseState<EnemyCtrl>
{


    public float timeCheckWait = 0.5f;
    public float count = 0;
    public void EnterState(EnemyCtrl enemy)
    {
        enemy.ChangeAnim(Constant.ANIM_IDLE);
    }
    public void Update(EnemyCtrl enemy)
    {
        if (GameManager.Instance.currentState != GameState.InGame)
        {
            enemy.ChangeState(enemy.pause);
            return;
        }
        count += Time.deltaTime;
        if (enemy.CheckEnemy() && enemy.weaponImg.activeSelf)
        {
            enemy.ChangeState(enemy.attack);
            return;
        }
        if (count >= timeCheckWait)
        {
            count = 0;
            enemy.ChangeState(enemy.move);
        }
    }
    public void ExitState(EnemyCtrl enemy)
    {

    }
}
