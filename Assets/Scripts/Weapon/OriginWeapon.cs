using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OriginWeapon : GameUnit
{
    public Character owner;
    public float speed;
    public Vector3 direct;

    public override void OnInit()
    {
        
    }
    public override void OnDespawn()
    {
       SimplePool.Despawn(this);
    }
}
