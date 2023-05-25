using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    }
   

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.owner.gameObject) return;
        if (other.CompareTag(Constant.CHARACTER))
        {
            this.AddPoint(1);
            owner.GrowthCharacter();
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
    protected  void MoveToEnemy()
    {

        TF.position += direct * speed * Time.deltaTime;
        if (Vector3.Distance(TF.position, this.owner.TF.position) >= this.owner.rangeAttack)
        {
            this.EndAttack();
        }
    }
    protected void Rotate()
    {
        int speed = 500;
        TF.Rotate(0f, speed * Time.deltaTime, 0f, Space.World);
    }
    protected void DirectToTarget()
    {
        TF.rotation = this.owner.TF.rotation;
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
    private void AddPoint(int addPoint)
    {
        owner.point += addPoint;
        owner.wayPoint.UpdatePoint(owner.point);
    }
}
