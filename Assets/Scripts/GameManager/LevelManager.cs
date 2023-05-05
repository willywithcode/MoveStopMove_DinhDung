using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private EnemyCtrl enemyPrefab;
    //[SerializeField] private ThrowWeapon weaponPrefab;
    [SerializeField] private PlayerCtrl player;

    public List<GameObject> prefabsLevelState;


    //private List<EnemyCtrl> bots = new List<EnemyCtrl>();

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            EnemyCtrl bot = SimplePool.Spawn<EnemyCtrl>(enemyPrefab);
            bot.OnInit();
        }
    }
}
