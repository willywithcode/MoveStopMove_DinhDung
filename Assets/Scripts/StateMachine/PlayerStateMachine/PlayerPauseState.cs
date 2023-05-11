using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseState : BaseState<PlayerCtrl>
{
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.joystick.enabled= false;
    }
    public void Update(PlayerCtrl ctrl)
    {
        if (GameManager.Instance.currentState == GameState.InGame) ctrl.ChangeState(ctrl.idle);
    }
    public void ExitState(PlayerCtrl ctrl)
    {
        ctrl.joystick.enabled= true;
    }
}
