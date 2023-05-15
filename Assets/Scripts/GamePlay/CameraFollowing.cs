using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowing : Singleton<CameraFollowing>
{
    [SerializeField] private Transform player;

    private float speed;
    private Transform TF;
    void Start()
    {
        speed = 5;   
        TF = transform;
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameState.ShopSkinMenu)
        {
            TF.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 15, -15), Time.deltaTime * speed);
            TF.rotation = Quaternion.Euler(45, 0, 0);
            return;
        }
        TF.position = player.position + new Vector3(0,-1.551f,-10);
        TF.rotation = Quaternion.Euler(0, 0, 0);
    }
}
