using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : BaseState<PlayerCtrl>
{
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.ChangeAnim(Constant.ANIM_DEAD);
        ctrl.collider.enabled = false;
        ctrl.joystick.enabled = false;
        ctrl.direct = Vector3.zero;
    }
    public void Update(PlayerCtrl ctrl)
    {
        if (GameManager.Instance.currentState != GameState.InGame)
        {
            ctrl.ChangeState(ctrl.pause);
            return;
        }
    }
    public void ExitState(PlayerCtrl ctrl)
    {
        ctrl.collider.enabled = true;
        ctrl.joystick.enabled = true;

    }
}
