using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState<PlayerCtrl>
{
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.ChangeAnim(Constant.ANIM_ATTACK);
    }
    public void Update(PlayerCtrl ctrl)
    {
        ctrl.Attack();
        ctrl.timeCountAttack += Time.deltaTime;
        if (ctrl.timeCountAttack >= ctrl.timeLimitAttack)
        {
            ctrl.timeCountAttack = 0;
            ctrl.ChangeState(ctrl.idle);
        }
        if ((Vector3.Distance(ctrl.direct, Vector3.zero) >= 0.001f)) ctrl.ChangeState(ctrl.move);
    }
}
