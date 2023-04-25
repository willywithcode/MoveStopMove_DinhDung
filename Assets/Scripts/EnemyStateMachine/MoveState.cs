using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : BaseState
{
    public override void EnterState(EnemyCtrl enemy)
    {
        enemy.animator.SetBool("IsUlti", true);
        enemy.animator.SetBool("IsIdle", false);
        this.Move(enemy);
    }
    public override void Update(EnemyCtrl enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.agent.destination) <= 0.1)
        {
            this.Move(enemy);
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
