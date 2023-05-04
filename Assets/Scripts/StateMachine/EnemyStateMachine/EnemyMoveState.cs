using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : BaseState<EnemyCtrl> 
{
    public void EnterState(EnemyCtrl enemy)
    {
        enemy.ChangeAnim(Constant.ANIM_RUN);
        this.Move(enemy);
    }
    public void Update(EnemyCtrl enemy)
    {

        if (Vector3.Distance(enemy.transform.position, enemy.agent.destination) <= 0.01)
        {
            enemy.ChangeState(enemy.idle);
        }
    }

    private void Move(EnemyCtrl enemy)
    {
        Vector3 currentPosition = enemy.transform.position;
        Vector3 randomDes = Random.insideUnitSphere * 10f + currentPosition;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDes, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            randomDes = hit.position;
        }
        enemy.agent.SetDestination(randomDes);
    }
}