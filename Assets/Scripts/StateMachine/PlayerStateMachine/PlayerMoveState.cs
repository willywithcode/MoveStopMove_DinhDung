using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState<PlayerCtrl>
{
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.ChangeAnim(Constant.ANIM_RUN);
    }
    public void Update(PlayerCtrl ctrl)
    {
        if (GameManager.Instance.currentState != GameState.InGame)
        {
            ctrl.ChangeState(ctrl.pause);
            return;
        }
        this.Run(ctrl);
    }
    public void ExitState(PlayerCtrl ctrl)
    {

    }
    private void Run(PlayerCtrl ctrl)
    {
        ctrl.transform.position += ctrl.direct * ctrl.speed * Time.deltaTime;
        float angle = Mathf.Atan2(ctrl.direct.x, ctrl.direct.z) * Mathf.Rad2Deg;
        if (angle != 0) ctrl.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        if (Vector3.Distance(ctrl.direct, Vector3.zero) <= 0.001f) ctrl.ChangeState(ctrl.idle);
    }
}
