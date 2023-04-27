using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    public List<GameObject> prefabsLevelState;

    private List<EnemyCtrl> bots = new List<EnemyCtrl>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            EnemyCtrl bot = SimplePool.Spawn<EnemyCtrl>(PoolType.EnemyCtrl);
            bot.OnInit();
            bots.Add(bot);
        }
    }
}
