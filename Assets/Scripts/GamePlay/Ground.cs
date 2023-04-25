using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ground : MonoBehaviour
{
    public Bounds boundLimit;
    private Vector3 playerPosition;
    void Start()
    {
        boundLimit.center = transform.position;
        boundLimit.extents = new Vector3(25, 0, 25);
    }

    
}
