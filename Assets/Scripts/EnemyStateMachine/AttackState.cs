using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : BaseState
{
    public override void EnterState(EnemyCtrl enemy)
    {
        enemy.currentState = this;
        enemy.agent.SetDestination(enemy.transform.position);
        enemy.Attack();
    }
    public override void Update(EnemyCtrl enemy)
    {

    }
}