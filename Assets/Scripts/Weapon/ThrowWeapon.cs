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
        direct = this.owner.positionTarget - this.owner.transform.position;
        direct.Normalize();
    }
    void Update()
    {
        if (!isHitObtacle)this.MoveWeapon();
        else this.WaitForAttach();
    }
    protected override void MoveWeapon()
    {
        base.MoveWeapon();
        transform.position  += direct * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, this.owner.transform.position) >= this.owner.rangeAttack )
        {
            this.EndAttack();
        }
    }
}
