using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BloodExplode : GameUnit
{
    [SerializeField] private ParticleSystem particleSystem;
    private float count;
    private float timeBloodExplosion = 0.2f;
    public override void OnInit()
    {
        count = 0;
        particleSystem.Play();
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void Update()
    {
        count += Time.deltaTime;
        if (count > timeBloodExplosion) this.OnDespawn();
    }
}
