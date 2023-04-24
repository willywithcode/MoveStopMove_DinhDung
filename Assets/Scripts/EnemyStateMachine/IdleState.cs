using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
   protected override void EnterState(EnemyCrl enemy)
    {
        enemy.animator.SetBool("IsIdle", true);
    }
}
