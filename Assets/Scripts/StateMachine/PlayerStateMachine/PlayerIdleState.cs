using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState<PlayerCtrl>
{
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.ChangeAnim(Constant.ANIM_IDLE);
    }
    public void Update(PlayerCtrl ctrl)
    {
        if (GameManager.Instance.currentState != GameState.InGame)
        {
            ctrl.ChangeState(ctrl.pause);
            return;
        }
        if (ctrl.CheckEnemy() && ctrl.weaponImg.activeSelf) ctrl.ChangeState(ctrl.attack);
        else if ((Vector3.Distance(ctrl.direct, Vector3.zero) >= 0.001f)) ctrl.ChangeState(ctrl.move);
    }
    public void ExitState(PlayerCtrl ctrl)
    {

    }
}
