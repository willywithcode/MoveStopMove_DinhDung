using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystemManager : Singleton<EffectSystemManager>
{
    [SerializeField] private GameUnit prefabBloodExplodePS;
    [SerializeField] private Transform parentContainer;
    private void Start()
    {
        SimplePool.Preload(prefabBloodExplodePS,30, parentContainer,true,false);
        this.RegisterListener(EventID.OnWeaponHitEnemy, (param) => ExplodeBlood((OriginWeapon) param));
    }
    public void ExplodeBlood(OriginWeapon weapon)
    {
        BloodExplode bloodExplode = SimplePool.Spawn<BloodExplode>(prefabBloodExplodePS);
        bloodExplode.TF.position = weapon.victim.bloodExplosionContainer.position;
        bloodExplode.TF.localScale = Vector3.one * weapon.victim.scaleGrowth;
        bloodExplode.OnInit();
    }
}