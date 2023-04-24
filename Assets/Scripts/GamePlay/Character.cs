using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;


    protected float rangeAttack;
    protected float scaleGrowth;
    protected float weapon;
    protected float speed;
    protected float timeLimitAttack = 1;
    protected float timeCount = 0;
}
