using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : OriginWeapon
{
    public override void OnInit()
    {
        base.OnInit();
        direct = (this.owner.positionTarget - this.owner.transform.position);
        direct.y = 0;
    }
    void Update()
    {
        this.MoveWeapon();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.owner.gameObject) return;
        if(other.CompareTag(Constant.CHARACTER))
        {
            this.EndAttack();
            EnemyCtrl enemy =  other.GetComponent<EnemyCtrl>();
            enemy.ChangeState(enemy.dead);
        }
        else if (other.CompareTag(Constant.PLAYER))
        {
            this.EndAttack();
            PlayerCtrl playerCrtl = other.GetComponent<PlayerCtrl>();
            playerCrtl.ChangeState(playerCrtl.dead);
        }
    }
    private void EndAttack()
    {
        this.OnDespawn();
        this.owner.isAttack = false;
        this.owner.weaponImg.SetActive(true);
    }

    private void MoveWeapon()
    {
        this.transform.position += direct * Time.deltaTime;
        if (Vector3.Distance(transform.position, this.owner.transform.position) >= this.owner.rangeAttack)
        {
            this.EndAttack();
        }
    }
}
