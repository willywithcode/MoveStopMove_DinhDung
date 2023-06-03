using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillManager : Singleton<KillManager> 
{

    public UnityAction<OriginWeapon> OnKillEvent;
    private void Start()
    {
        this.RegisterListener(EventID.OnWeaponHitEnemy,(param) => ReportKill((OriginWeapon) param));
    }
    public void ReportKill(OriginWeapon weapon)
    {
        OnKillEvent?.Invoke(weapon); 
    }
}
