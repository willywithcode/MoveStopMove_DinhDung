using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Character : GameUnit
{
    [SerializeField] protected LayerMask layerCharacter;
    [SerializeField] protected ThrowWeapon weaponPrefab;

    public Animator animator;
    public GameObject weaponImg;
    public OriginWeapon weaponCrl;
    public float rangeAttack;
    public float timeLimitAttack = 0.85f;
    public float timeCountAttack = 0;
    public Vector3 positionTarget;
    public float speed;
    public NavMeshAgent agent;

    protected Character target;
    protected float scaleGrowth;
    [SerializeField]protected string currentAnimName;

    public void Awake()
    {
        OnInit();
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    public bool CheckEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack, layerCharacter);
        Array.Sort(enemies, new ColliderDistanceComparer(transform.position));
        if (enemies.Length > 1)
        {
            target = Cache.GetScript(enemies[1]);
            positionTarget = target.transform.position;
            Vector3 targetAngle = positionTarget - transform.position;
            float targetAngleY = Mathf.Atan2(targetAngle.x, targetAngle.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngleY, 0f);
        }
        return enemies.Length > 1;
    }
    //public void DefindTarger()
    //{

    //    Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack, layerCharacter);
       
    //}
    

    public override void OnInit()
    {
        currentAnimName = Constant.ANIM_IDLE;
    }


    public override void OnDespawn()
    {
        
        SimplePool.Despawn(this);
    }
    protected internal void SpawnWeapon()
    {
        ThrowWeapon weapon = SimplePool.Spawn<ThrowWeapon>(weaponPrefab);
        weapon.owner = this;
        weapon.OnInit();
        weapon.transform.position = this.transform.position + Vector3.up * 1f;
    }
}



public class ColliderDistanceComparer : IComparer<Collider>
{
    private Vector3 m_ComparePosition;

    public ColliderDistanceComparer(Vector3 comparePosition)
    {
        m_ComparePosition = comparePosition;
    }

    public int Compare(Collider x, Collider y)
    {
        float xDistance = Vector3.Distance(m_ComparePosition, x.transform.position);
        float yDistance = Vector3.Distance(m_ComparePosition, y.transform.position);
        return xDistance.CompareTo(yDistance);
    }
}