using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public MissionWayPoint wayPoint;
    public Renderer skinColor;

    public Animator animator;
    public Transform wayPointTarget;
    public Transform nameBoardTarget;

    public float scaleGrowth;
    public float rangeAttack;
    public float speed;
    public int point;
    public int defeatPoint;

    public Vector3 positionTarget;
    public NavMeshAgent agent;
    public Character target;
    protected string currentAnimName;

    public NameBoard nameUI;
    public string namePlayer;

    public  void Awake()
    {
        //OnInit();
    }
    public override void OnInit()
    {
        currentAnimName = Constant.ANIM_IDLE;
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
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
    
    public void SpawnNewWayPoint()
    {
        if (GameManager.Instance.currentState != GameState.InGame) return;
        wayPoint = SimplePool.Spawn<MissionWayPoint>(LevelManager.Instance.wayPointPrfab);
        wayPoint.OnInit();
        wayPoint.target = this.wayPointTarget;
        wayPoint.color.color = skinColor.material.color;

        nameUI = SimplePool.Spawn<NameBoard>(WayPointManager.Instance.prefabNameBoard);
        nameUI.OnInit();
        nameUI.target = this.nameBoardTarget;
        nameUI.nameChar.text = Name.GetName();
        namePlayer = nameUI.nameChar.text;
    }
    public virtual void GrowthCharacter()
    {
        for (int i = 0; i < LevelManager.Instance.pointStones.Count -1; i++)
        {
            if (point >= LevelManager.Instance.pointStones[i].point && point < LevelManager.Instance.pointStones[i + 1].point)
            {
                scaleGrowth = LevelManager.Instance.pointStones[i].scale;
                defeatPoint = LevelManager.Instance.pointStones[i].defeatPoint;
            }
            if (point >= LevelManager.Instance.pointStones.Last().point)
            {
                scaleGrowth = LevelManager.Instance.pointStones.Last().scale;
                defeatPoint = LevelManager.Instance.pointStones.Last().defeatPoint;
            }
        }
        this.TF.localScale = Vector3.one * scaleGrowth;
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