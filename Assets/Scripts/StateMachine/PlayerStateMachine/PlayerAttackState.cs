using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerAttackState : BaseState<PlayerCtrl>
{

    public float timeCount = 0;
    public float timeSkill = 0.4f;
    public float timeLimitAttack = 0.85f;
    private bool isThrowing;
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.ChangeAnim(Constant.ANIM_ATTACK);
        this.timeCount = 0;
        isThrowing = false;
    }
    public void Update(PlayerCtrl ctrl)
    {
        if (GameManager.Instance.currentState != GameState.InGame)
        {
            ctrl.ChangeState(ctrl.pause);
            return;
         }
        timeCount += Time.deltaTime;
        this.CountDownAttack(ctrl);
        if (timeCount >= timeLimitAttack )
        {
            ctrl.ChangeState(ctrl.idle);
        }
        else if ((Vector3.Distance(ctrl.direct, Vector3.zero) >= 0.001f)) 
        {
            ctrl.ChangeState(ctrl.move);
        }
    }
    public void ExitState(PlayerCtrl ctrl)
    {

    }
    private void CountDownAttack(PlayerCtrl ctrl)
    {
        if (timeCount >= timeSkill && !isThrowing)
        {
            ctrl.Rotate();
            isThrowing = true;
            ctrl.weaponImg.SetActive(false);
            EquipmentManager.Instance.SpawnWeapon(ctrl);
            if (!ctrl.isUlti) return;
            ctrl.ResetRange();
            ctrl.ScaleRangeCircleGift(1);
        }
    }
}
