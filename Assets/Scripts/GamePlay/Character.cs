using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class Character : GameUnit
{
    [SerializeField] protected LayerMask layerCharacter;
    [SerializeField] protected List<WeaponImg> listWeapon;

    public GameObject weaponImg;
    public PoolType typeWeapon;
    public EquipmentSO weaponData;
    public Transform hatContainer;
    public Transform shieldContainer;
    public Renderer pantType;

    public Animator animator;
    public float rangeAttack;
    public Vector3 positionTarget;
    public float speed;
    public NavMeshAgent agent;
    public Character target;

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
    public void RandomWeapon()
    {
        PoolType[] allColors = (PoolType[])Enum.GetValues(typeof(PoolType));
        typeWeapon = allColors[UnityEngine.Random.Range(1,allColors.Length)];
    }
    public void AssignWeapon()
    {
        for(int i = 0; i < listWeapon.Count; i++)
        {
            WeaponImg weapon = listWeapon[i];
            if (weapon.weaponType == typeWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else weapon.gameObject.SetActive(false);
        }
    }
    public void ClearOldWeapon()
    {
        for (int i = 0; i < listWeapon.Count; i++)
        {
            WeaponImg weapon = listWeapon[i];
            if (weapon.gameObject.activeSelf)
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
    public bool CheckEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeAttack, layerCharacter);
        Array.Sort(enemies, new ColliderDistanceComparer(transform.position));
        if (enemies.Length > 1)
        {
            target = Cache.GetScript(enemies[1]);
            this.Rotate();
        }
        return enemies.Length > 1;
    }
    public void Rotate()
    {
        positionTarget = target.TF.position;
        Vector3 targetAngle = positionTarget - TF.position;
        float targetAngleY = Mathf.Atan2(targetAngle.x, targetAngle.z) * Mathf.Rad2Deg;
        TF.rotation = Quaternion.Euler(0f, targetAngleY, 0f);
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngleY, 0f);
        float rotationSpeed = 50f; 
        TF.rotation = Quaternion.Slerp(TF.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }
    public override void OnInit()
    {
        currentAnimName = Constant.ANIM_IDLE;
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