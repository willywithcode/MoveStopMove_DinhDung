using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private PlayerCtrl player;

    public List<GameObject> prefabsLevelState;
    public int countCharacterCurrent = 0;
    

    private int maxCharacter = 50; 
    private int countCharacter = 0;
    private int maxCharacterCurrent = 15;


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
        if (GameManager.Instance.currentState != GameState.InGame && GameManager.Instance.currentState != GameState.Question && GameManager.Instance.currentState != GameState.Pause) player.attackRoundObject.SetActive(false);
        else player.attackRoundObject.SetActive(true);
    }
    private Vector3 RandomPos()
    {
        Vector3 currentPosition = player.TF.position;
        Vector3 randomPos = Random.insideUnitSphere * 40f + currentPosition;
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            randomPos = hit.position;
        }
        return randomPos;
    }
    private void SpawnEnemy()
    {
        EnemyCtrl bot = SimplePool.Spawn<EnemyCtrl>(PoolType.EnemyCtrl);
        bot.OnInit();
        bot.TF.position = RandomPos();
        countCharacter++;
        countCharacterCurrent++;
    }
}
