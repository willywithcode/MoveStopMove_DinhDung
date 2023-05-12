using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThrowWeapon : OriginWeapon
{
    public override void OnInit()
    {
        base.OnInit();
        speed = this.owner.rangeAttack*1.5f ;
        direct = this.owner.positionTarget - this.owner.TF.position;
        direct.Normalize();
        this.DirectToTarget();
    }
    void Update()
    {
        if (!isHitObtacle)
        {
            this.MoveToEnemy();
        }
        else this.WaitForAttach();
    }
}
