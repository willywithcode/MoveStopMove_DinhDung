using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{

    public bool isHavingKilled;
    private string killer;
    private string victim;
    public void AlermMassageKill(string kill, string killed)
    {
        isHavingKilled = true;
        killer = kill;
        victim = killed;
    }
    public string GetMessageKill()
    {
        return killer + Constant.killAlermMessage + victim;
    }
}
