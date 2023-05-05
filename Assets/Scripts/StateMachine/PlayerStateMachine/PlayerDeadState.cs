using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : BaseState<PlayerCtrl>
{
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.ChangeAnim(Constant.ANIM_DEAD);
        ctrl.GetComponent<Collider>().enabled = false;
    }
    public void Update(PlayerCtrl ctrl)
    {

    }
}
