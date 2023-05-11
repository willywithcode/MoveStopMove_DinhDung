using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : BaseState<EnemyCtrl>
{
    private float timeDestroy = 1.5f;
    private float count = 0;
    public void EnterState(EnemyCtrl enemy)
    {
        enemy.ChangeAnim(Constant.ANIM_DEAD);
        enemy.GetComponent<Collider>().enabled = false;
        enemy.agent.SetDestination(enemy.transform.position);
    }
    public void Update(EnemyCtrl enemy)
    {
        if (GameManager.Instance.currentState != GameState.InGame)
        {
            enemy.ChangeState(enemy.pause);
            return;
        }
        count += Time.deltaTime;
        if(count >= timeDestroy)
        {
            count = 0;
            enemy.OnDespawn();
        }
    }
    public void ExitState(EnemyCtrl enemy)
    {

    }
}
