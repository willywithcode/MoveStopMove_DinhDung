using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystemManager : Singleton<EffectSystemManager>
{
    [SerializeField] private GameUnit prefabBloodExplodePS;
    [SerializeField] private Transform parentContainer;
    private void Start()
    {
        SimplePool.Preload(prefabBloodExplodePS,15, parentContainer,true,false);
    }
    public void ExplodeBlood(Character enemy)
    {
        BloodExplode bloodExplode = SimplePool.Spawn<BloodExplode>(prefabBloodExplodePS);
        bloodExplode.TF.position = enemy.bloodExplosionContainer.position;
        bloodExplode.TF.localScale = Vector3.one * enemy.scaleGrowth;
        bloodExplode.OnInit();
    }
}
