using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
   public override void EnterState(EnemyCtrl enemy)
    {
        enemy.animator.SetBool(Constant.ANIM_IDLE, true);
    }
    public override void Update(EnemyCtrl enemy)
    {

    }
}
