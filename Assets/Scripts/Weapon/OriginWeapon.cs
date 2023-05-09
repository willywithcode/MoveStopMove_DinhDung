using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OriginWeapon : GameUnit
{
    public Character owner;
    public float speed;
    public Vector3 direct;

    protected bool isHitObtacle;
    protected float count = 0;
    protected float timeAttach = 0.5f;

    public override void OnInit()
    {
        
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        //Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.owner.gameObject) return;
        if (other.CompareTag(Constant.CHARACTER))
        {
            this.EndAttack();
            EnemyCtrl enemy = other.GetComponent<EnemyCtrl>();
            enemy.ChangeState(enemy.dead);
            LevelManager.Instance.countCharacterCurrent--;
        }
        //else if (other.CompareTag(Constant.PLAYER))
        //{
        //    this.EndAttack();
        //    PlayerCtrl playerCrtl = other.GetComponent<PlayerCtrl>();
        //    playerCrtl.ChangeState(playerCrtl.dead);
        //}
        else if (other.CompareTag(Constant.OBTACLE)) isHitObtacle = true;
    }
    protected virtual void MoveWeapon()
    {

    }
    protected void EndAttack()
    {
        this.OnDespawn();
        this.owner.weaponImg.SetActive(true);
    }
    protected void WaitForAttach()
    {
        count += Time.deltaTime;
        if (count >= timeAttach) this.EndAttack();
    }



}
