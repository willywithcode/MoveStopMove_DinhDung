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
        this.transform.position += direct * Time.deltaTime;
        if (Vector3.Distance(transform.position, this.owner.transform.position) >= this.owner.rangeAttack) { 
            this.OnDespawn();
            this.owner.isAttack = false;
            this.owner.weaponImg.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.CompareTag(Constant.CHARACTER))
        {
            this.OnDespawn();
            this.owner.isAttack = false;
            this.owner.weaponImg.SetActive(true);
            EnemyCtrl enemy =  other.GetComponent<EnemyCtrl>();
            enemy.ChangeState(enemy.dead);
            Debug.Log(2);
        }
        else if (other.CompareTag(Constant.PLAYER))
        {
            this.OnDespawn();
            this.owner.isAttack = false;
            this.owner.weaponImg.SetActive(true);
            PlayerCtrl playerCrtl = other.GetComponent<PlayerCtrl>();
            playerCrtl.ChangeState(playerCrtl.dead);
            Debug.Log(1);
        }
    }
}
