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
        //ThrowWeapon weaponPlayer = SimplePool.Spawn<ThrowWeapon>(weaponPrefab);
        //player.weaponCrl= weaponPlayer;
        //weaponPlayer.owner = player;
        //weaponPlayer.OnInit();

        for (int i = 0; i < 3; i++)
        {
            EnemyCtrl bot = SimplePool.Spawn<EnemyCtrl>(enemyPrefab);
            //ThrowWeapon weapon = SimplePool.Spawn<ThrowWeapon>(weaponPrefab);
            bot.OnInit();
            //bot.weaponCrl = weapon;
            //weapon.owner = bot;
            //weapon.OnInit();
        }
    }
}
