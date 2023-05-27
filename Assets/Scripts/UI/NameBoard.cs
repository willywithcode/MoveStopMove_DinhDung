using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameBoard : GameUnit
{
    public TextMeshProUGUI nameChar;
    public Transform target;

    private void Update()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        this.transform.position = pos;
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public override void OnInit()
    {

    }
}
