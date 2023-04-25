using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected LayerMask layerCharacter;

    public Animator animator;
    public GameObject weapon;
    public float rangeAttack;
    public float timeSkill = 0.45f;
    public float timeLimitAttack = 1f;
    public float timeCount = 0;

    [SerializeField] protected Character target;
    protected Vector3 positionTarget;
    protected float scaleGrowth;
    protected float speed;
    protected OriginWeapon weaponCrl;

    public void Awake()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        weaponCrl = weapon.GetComponent<OriginWeapon>();
        weaponCrl.owner = this.gameObject;
    }
}
