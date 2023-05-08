using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttackState : BaseState<PlayerCtrl>
{

    public float timeSkill = 0.4f;
    public float timeCountSkill = 0;

    private bool isThrowing;
    public void EnterState(PlayerCtrl ctrl)
    {
        ctrl.ChangeAnim(Constant.ANIM_ATTACK);
        ctrl.timeCountAttack = 0;
        this.timeCountSkill = 0;
        isThrowing = false;
    }
    public void Update(PlayerCtrl ctrl)
    {
        this.CountDownAttack(ctrl);
        ctrl.timeCountAttack += Time.deltaTime;
        if (ctrl.timeCountAttack >= ctrl.timeLimitAttack )
        {
            ctrl.ChangeState(ctrl.idle);
        }
        else if ((Vector3.Distance(ctrl.direct, Vector3.zero) >= 0.001f)) 
        {
            ctrl.ChangeState(ctrl.move);
        }
    }
    private void CountDownAttack(PlayerCtrl ctrl)
    {
        timeCountSkill += Time.deltaTime;
        if (timeCountSkill >= timeSkill && !isThrowing)
        {

            ctrl.Rotate();
            isThrowing = true;
            ctrl.weaponImg.SetActive(false);
            WeaponManager.Instance.SpawnWeapon(ctrl,ctrl.weaponData.weaponCtrl);
        }
    }
}
