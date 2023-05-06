using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThrowWeapon : OriginWeapon
{
    public override void OnInit()
    {
        base.OnInit();
        speed = this.owner.rangeAttack*1.5f ;
        direct = this.owner.positionTarget - this.owner.transform.position;
        direct.Normalize();
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
            LevelManager.Instance.countCharacterCurrent--;
        }
        //else if (other.CompareTag(Constant.PLAYER))
        //{
        //    this.EndAttack();
        //    PlayerCtrl playerCrtl = other.GetComponent<PlayerCtrl>();
        //    playerCrtl.ChangeState(playerCrtl.dead);
        //}
    }
    private void EndAttack()
    {
        this.OnDespawn();
        this.owner.weaponImg.SetActive(true);
    }

    private void MoveWeapon()
    {
        transform.position  += direct * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, this.owner.transform.position) >= this.owner.rangeAttack )
        {
            this.EndAttack();
        }
    }
}
