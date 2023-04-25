using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : BaseState
{
    public override void EnterState(EnemyCtrl enemy)
    {
        enemy.agent.SetDestination(enemy.transform.position);
        enemy.animator.SetBool("IsAttack", true);
        enemy.animator.SetBool("IsIdle", false);
        enemy.timeCount += Time.deltaTime;
        if (enemy.timeCount >= enemy.timeSkill) enemy.weapon.SetActive(false);
    }
    public override void Update(EnemyCtrl enemy)
    {

    }
}
