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
            SimplePool.Preload(prefabWayPoint, 30, containerWayPoint, true, false);
            SimplePool.Preload(prefabNameBoard, 30, containerNameBoard, true, false);
    }
}
