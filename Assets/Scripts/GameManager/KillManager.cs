using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillManager : Singleton<KillManager> 
{

    public UnityAction OnKillEvent; 
    public void ReportKill(string killer, string victim)
    {
        PopupManager.Instance.NameKiller = killer;
        PopupManager.Instance.NameVictim = victim;
        OnKillEvent?.Invoke(); 
    }
}
