using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : Singleton<CameraFollowing>
{
    [SerializeField] private Transform player;

    private float speed;
    void Start()
    {
        speed = 5;   
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 15, -15), Time.deltaTime * speed);
        transform.transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
