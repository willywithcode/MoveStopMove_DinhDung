using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    public int segments = 360;
    public float radius;

    [SerializeField] private LineRenderer line;

    void Awake()
    {
        radius = 5f;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        Draw();
    }

    void Draw()
    {
        float angle = 0f;
        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius * 0.83f;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius * 0.83f;

            line.SetPosition(i, new Vector3(x, y, 0f));

            angle += (360f / segments);
        }
    }
    public void ChangeAttackRange(float range)
    {
        float angle = 0f;
        radius = range;
        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius * 0.83f;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius * 0.83f;

            line.SetPosition(i, new Vector3(x, y, 0f));

            angle += (360f / segments);
        }
    }
}
