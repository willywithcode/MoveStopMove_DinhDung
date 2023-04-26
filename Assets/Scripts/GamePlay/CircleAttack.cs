using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    public int segments = 360;
    public float radius = 10f;

    [SerializeField] private LineRenderer line;

    void Start()
    {
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        Draw();
    }

    void Draw()
    {
        float angle = 0f;
        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0f));

            angle += (360f / segments);
        }
    }
}
