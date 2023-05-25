using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionWayPoint : GameUnit
{
    [SerializeField] private Image image;
    public Transform target;
    public override void OnInit()
    {

    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void Update()
    {
        float minX = image.GetPixelAdjustedRect().width/2;
        float maxX = Screen.width - minX;

        float minY = image.GetPixelAdjustedRect().height/2;
        float maxY = Screen.height - minY;
        Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position);

        if (Vector3.Dot((target.position -  TF.position), TF.forward) < 0)
        {
            if ( pos.x < Screen.width /2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        image.transform.position = pos;
    }
}
