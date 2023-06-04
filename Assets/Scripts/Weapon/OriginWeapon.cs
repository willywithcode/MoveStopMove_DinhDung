using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OriginWeapon : GameUnit
{
    public Character owner;
    public Character victim;
    public float speed;
    public Vector3 direct;

    protected bool isHitObtacle;
    protected float count;
    protected float timeAttach = 0.5f;
    protected float elapsedTime;
    protected float duration = 2;
    protected Vector3 initialScale;
    public override void OnInit()
    {
        this.count = 0;
        elapsedTime = 0;
        this.victim= null;
        this.TF.localScale = Vector3.one * owner.scaleGrowth;
        initialScale = this.TF.localScale;
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
            this.victim = Cache.GetScript(other);
            this.PostEvent(EventID.OnWeaponHitEnemy,this);
        }
        else if (other.CompareTag(Constant.PLAYER))
        {
            this.victim = LevelManager.Instance.Player;
            this.PostEvent(EventID.OnPlayerDie,this);
        }
        else if (other.CompareTag(Constant.OBTACLE)) isHitObtacle = true;
    }
    #region Movement
    protected void MoveToEnemy()
    {
        TF.position += direct * speed * Time.deltaTime;
        if (Vector3.Distance(TF.position, this.owner.TF.position) >= this.owner.rangeAttack && !owner.isUlti)
        {
            this.EndAttack();
        }
        if (owner.isUlti)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(initialScale, 5 * initialScale, t);
            if (Vector3.Distance(transform.localScale ,initialScale *5) <= 0.1f)
            {
                this.EndAttack();
                owner.isUlti = false;
            }
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
    #endregion
    #region Condition
    public void EndAttack()
    {
        this.OnDespawn();
        owner.weaponImg.SetActive(true);
    }
    protected void WaitForAttach()
    {
        count += Time.deltaTime;
        if (count >= timeAttach)
        {
            this.EndAttack();
            this.isHitObtacle = false;
        }
    }
    #endregion
}