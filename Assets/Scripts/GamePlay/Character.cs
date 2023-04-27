using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : GameUnit
{
    [SerializeField] protected LayerMask layerCharacter;

    public Animator animator;
    public GameObject weapon;
    public float rangeAttack;
    public float timeSkill = 0.45f;
    public float timeCountSkill = 0;

    protected Character target;
    protected Vector3 positionTarget;
    protected float scaleGrowth;
    protected float speed;
    protected OriginWeapon weaponCrl;
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
        return enemies.Length > 1;
    }

    public void Attack()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack, layerCharacter);
        Array.Sort(enemies, new ColliderDistanceComparer(transform.position));
        if (enemies.Length > 1)
        {
            ChangeAnim(Constant.ANIM_ATTACK);
            timeCountSkill += Time.deltaTime;
            if (timeCountSkill >= timeSkill)
            {
                timeCountSkill = 0;
                weapon.SetActive(false);
            }
            target = Cache.GetScript(enemies[1]);
            positionTarget = target.transform.position;
            Vector3 targetAngle = positionTarget - transform.position;
            float targetAngleY = Mathf.Atan2(targetAngle.x, targetAngle.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngleY, 0f);
        }
    }

    public override void OnInit()
    {
        currentAnimName = Constant.ANIM_IDLE;
        weaponCrl = weapon.GetComponent<OriginWeapon>();
        weaponCrl.owner = this.gameObject;
    }

    public override void OnDespawn()
    {
        
        SimplePool.Despawn(this);
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