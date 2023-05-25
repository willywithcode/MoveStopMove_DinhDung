using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MissionWayPoint : GameUnit
{
    [SerializeField] private GameObject image;
    [SerializeField] private RectTransform sizeImage;
    [SerializeField] private GameObject arrow;
    public TextMeshProUGUI point;
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
        float minX = sizeImage.rect.width/2;
        float maxX = Screen.width - minX;

        float minY = sizeImage.rect.height/2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        Vector2 direction = pos - new Vector2(Screen.width/2,Screen.height/2);
        float angle = Vector2.Angle(direction, Vector3.right);

        if (Vector3.Dot((target.position -  TF.position), TF.forward) < 0)
        {
            if ( pos.x < Screen.width /2) pos.x = maxX;
            else pos.x = minX;
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        if (pos.x == maxX || pos.y == maxY || pos.x == minX || pos.y == minY)
        {
            arrow.SetActive(true);
            image.transform.eulerAngles = Vector3.forward * angle;
        }
        else
        {
            arrow.SetActive(false);
            image.transform.eulerAngles = Vector3.zero;
        }
        this.transform.position = pos;
    }
    public void UpdatePoint(int pointCharacter)
    {
        point.text = pointCharacter.ToString();
    }
}
