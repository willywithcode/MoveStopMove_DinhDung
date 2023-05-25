using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : Singleton<WayPointManager>
{
    public MissionWayPoint prefabs;
    public Transform container;
    private void Awake()
    {
            SimplePool.Preload(prefabs, 25, container, false, false);
    }
}
