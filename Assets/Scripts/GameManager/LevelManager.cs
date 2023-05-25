using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
public struct PointStone
{
    public int point;
    public float scale;
}
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private PlayerCtrl player;
    public MissionWayPoint wayPointPrfab;

    public List<GameObject> prefabsLevelState;
    public List<EnemyCtrl> enemyCurrent;
    public List<PointStone> pointStones = new List<PointStone>();
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
        this.InitPointScale();
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
        if(GameManager.Instance.currentState == GameState.InGame)
        {
            this.SpawnWayPoint();
        }
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
        enemyCurrent.Add(bot);
        bot.OnInit();
        bot.TF.position = RandomPos();
        countCharacter++;
        countCharacterCurrent++;
    }
    private void SpawnWayPoint()
    {
        foreach (EnemyCtrl bot in enemyCurrent)
        {
            if (bot.wayPoint != null) return;
            bot.wayPoint = SimplePool.Spawn<MissionWayPoint>(wayPointPrfab);
            bot.wayPoint.OnInit();
            bot.wayPoint.target = bot.wayPointTarget;
        }
    }
    private void InitPointScale()
    {
        pointStones.Add(new PointStone { point = 2, scale = 1.2f });
        pointStones.Add(new PointStone { point = 6, scale = 1.5f });
        pointStones.Add(new PointStone { point = 10, scale = 1.8f });
        pointStones.Add(new PointStone { point = 15, scale = 2.1f });
        pointStones.Add(new PointStone { point = 22, scale = 2.5f });
    }
}
