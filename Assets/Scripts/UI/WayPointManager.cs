using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : Singleton<WayPointManager>
{
    public MissionWayPoint prefabWayPoint;
    public NameBoard prefabNameBoard;
    public Transform containerWayPoint;
    public Transform containerNameBoard;
    private void Awake()
    {
            SimplePool.Preload(prefabWayPoint, 50, containerWayPoint, true, false);
            SimplePool.Preload(prefabNameBoard, 50, containerNameBoard, true, false);
    }
}
