using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TxtKillUI : GameUnit
{
    [SerializeField]private TextMeshProUGUI txtContent;
    public string TxtContent
    {
        get { return txtContent.text; }
        set { txtContent.text = value; }
    } 
    public override void OnInit()
    {
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
