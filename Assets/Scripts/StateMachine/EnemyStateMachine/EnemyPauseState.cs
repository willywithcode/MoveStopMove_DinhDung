using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPauseState : BaseState<EnemyCtrl>
{
    public void EnterState(EnemyCtrl enemy)
    {
        enemy.agent.SetDestination(enemy.TF.position);
    }
    public void Update(EnemyCtrl enemy)
    {
        if (GameManager.Instance.currentState == GameState.InGame) enemy.ChangeState(enemy.idle);
    }
    public void ExitState(EnemyCtrl enemy)
    {

    }
}
