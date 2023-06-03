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
    public Image color;
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

        float minY = sizeImage.rect.width/2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        Vector2 direction = (Vector2)pos - new Vector2(Screen.width/2,Screen.height/2);
        float angle = Vector2.Angle(direction, Vector3.right);
        if (direction.y <= 0) angle  = 360 -angle;

        if (pos.z < 0)
        {
            pos.y = Screen.height - pos.y;
            pos.x = Screen.width - pos.x;
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
