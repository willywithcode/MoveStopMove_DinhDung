using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
public struct PointStone
{
    public int point;
    public float scale;
    public int defeatPoint;
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
    private int maxCharacterCurrent = 7;


    private void Start()
    {
        Name.RandomIndex();
        this.InitPointScale();
        UIManager.Instance.txtCoinCurrent.text = GameManager.Instance.currentCoin.ToString();
        for (int i = 0; i < maxCharacterCurrent; i++)
        {
            this.SpawnEnemy();
        }
    }
    private void Update()
    {
        this.CheckEnemyNum();
        this.CheckRangeCircle();
        this.CheckGameNamePlayer();
        this.CheckCoinDesplay();
    }
    private void CheckEnemyNum()
    {
        if (countCharacter >= maxCharacter) return;
        if (countCharacterCurrent < maxCharacterCurrent)
        {
            this.SpawnEnemy();
        }
    }
    private void CheckRangeCircle()
    {
        if (GameManager.Instance.currentState != GameState.InGame && GameManager.Instance.currentState != GameState.Question && GameManager.Instance.currentState != GameState.Pause) player.attackRoundObject.SetActive(false);
        else player.attackRoundObject.SetActive(true);
    }
    private void CheckGameNamePlayer()
    {
        if (GameManager.Instance.currentState == GameState.InGame)
        {
            if (!string.IsNullOrEmpty(player.nameUI.nameChar.text))
            {
                if (!string.IsNullOrEmpty(player.namePlayer))
                {
                    player.nameUI.nameChar.text = player.namePlayer;
                }
                else
                {
                    player.nameUI.nameChar.text = "You";

                }
            }
            this.SpawnWayPoint();
            this.SpawnNameBoard();
        }
    }
    private void CheckCoinDesplay()
    {
        if (GameManager.Instance.currentState == GameState.MainMenu || GameManager.Instance.currentState == GameState.ShopWeaponMenu || GameManager.Instance.currentState == GameState.ShopSkinMenu) UIManager.Instance.coinDesplayContainer.SetActive(true);
        else UIManager.Instance.coinDesplayContainer.SetActive(false);
    }
    private Vector3 RandomPos()
    {
        Vector3 currentPosition = new(player.TF.position.x, 0f, player.TF.position.z);
        Vector3 randomPos = Random.insideUnitSphere * 40f + currentPosition;
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            Vector3 tmp = hit.position;
            if (Vector3.Distance(new Vector3(tmp.x, 0, tmp.z), new Vector3(player.TF.position.x, 0, player.TF.position.z)) < 7f)
            {
                randomPos = this.RandomPos();
            }
            else randomPos = hit.position;
        }
        return randomPos;
    }
    private void SpawnEnemy()
    {
        EnemyCtrl bot = SimplePool.Spawn<EnemyCtrl>(PoolType.EnemyCtrl);
        bot.skinColor.material = ColorManager.Instance.SetRandomColor();
        enemyCurrent.Add(bot);
        bot.TF.position = RandomPos();
        bot.OnInit();
        countCharacter++;
        countCharacterCurrent++;
        if (GameManager.Instance.currentState == GameState.InGame)
        {
            bot.wayPoint.UpdatePoint(bot.point);
        }
    }
    public int RandomPoint()
    {
        int point = Random.Range(player.point - 3, player.point + 3);
        if (point <= 0) point = 1;
        return point;
    }
    private void SpawnWayPoint()
    {
        foreach (EnemyCtrl bot in enemyCurrent)
        {
            if (bot.wayPoint != null) return;
            bot.wayPoint = SimplePool.Spawn<MissionWayPoint>(wayPointPrfab);
            bot.wayPoint.OnInit();
            bot.wayPoint.UpdatePoint(bot.point);
            bot.wayPoint.target = bot.wayPointTarget;
            bot.wayPoint.color.color = bot.skinColor.material.color;
        }
    }
    private void SpawnNameBoard()
    {
        foreach (EnemyCtrl bot in enemyCurrent)
        {
            if (bot.nameUI != null) return;
            bot.nameUI = SimplePool.Spawn<NameBoard>(WayPointManager.Instance.prefabNameBoard);
            bot.nameUI.OnInit();
            bot.nameUI.target = bot.nameBoardTarget;
            bot.nameUI.nameChar.text = Name.GetName();
            bot.namePlayer = bot.nameUI.nameChar.text;
        }
    }
    private void InitPointScale()
    {
        pointStones.Add(new PointStone { point = 1, scale = 1f, defeatPoint = 1 });
        pointStones.Add(new PointStone { point = 2, scale = 1.2f , defeatPoint = 2});
        pointStones.Add(new PointStone { point = 6, scale = 1.5f , defeatPoint = 3 });
        pointStones.Add(new PointStone { point = 10, scale = 1.8f , defeatPoint = 4 });
        pointStones.Add(new PointStone { point = 15, scale = 2.1f , defeatPoint = 5 });
        pointStones.Add(new PointStone { point = 22, scale = 2.5f , defeatPoint = 6 });
    }
}
