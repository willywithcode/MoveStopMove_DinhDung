using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : OriginWeapon
{ 
    public override void OnInit()
    {
        base.OnInit();
        speed = this.owner.rangeAttack * 1.5f;
        direct = this.owner.positionTarget - this.owner.TF.position;
        direct.Normalize();
    }
    private void Update()
    {
        if (!isHitObtacle)
        {
            this.MoveToEnemy();
            this.Rotate();
        }
        else this.WaitForAttach();
    }
}
