using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private EnemyCtrl enemyPrefab;
    [SerializeField] private PlayerCtrl player;

    public List<GameObject> prefabsLevelState;
    public int countCharacterCurrent = 0;

    private int maxCharacter = 50; 
    private int countCharacter = 0;
    private int maxCharacterCurrent = 10;


    private void Start()
    {
        for (int i = 0; i < maxCharacterCurrent; i++)
        {
            this.SpawnEnemy();
        }
    }
    private void Update()
    {
        if (countCharacter >= maxCharacter) return;
        if (countCharacterCurrent < maxCharacterCurrent)
        {
            this.SpawnEnemy();
        }
    }
    private Vector3 RandomPos()
    {
        Vector3 currentPosition = player.transform.position;
        Vector3 randomPos = Random.insideUnitSphere * 20f + currentPosition;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            randomPos = hit.position;
        }
        return randomPos;
    }
    private void SpawnEnemy()
    {
        EnemyCtrl bot = SimplePool.Spawn<EnemyCtrl>(enemyPrefab);
        bot.OnInit();
        bot.transform.position = RandomPos();
        countCharacter++;
        countCharacterCurrent++;
    }
}
