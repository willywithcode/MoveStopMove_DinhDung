using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public PlayerCtrl Player { get { return player; } }
    public MissionWayPoint wayPointPrfab;

    public List<GameObject> prefabsLevelState;
    public List<Character> enemyCurrent;
    public List<PointStone> pointStones = new List<PointStone>();
    public int countCharacter = 0;
    

    private int maxCharacter = 99; 
    private int maxCharacterCurrent = 15 ;

    private void Awake()
    {
        this.RegisterListener(EventID.OnWeaponHitEnemy, (param) => OnWeaponHitEnemy((OriginWeapon)param));
        this.RegisterListener(EventID.OnEndGame, (param) => OnEndGame());
        this.RegisterListener(EventID.OnStartGame, (param) => OnStartGame());
        this.RegisterListener(EventID.OnPlayerDie, (param) => OnPlayerDie((OriginWeapon)param));
    }
    private void Start()
    {
        Name.RandomIndex();
        this.InitPointScale();
        UIManager.Instance.txtCoinCurrent.text = GameManager.Instance.currentCoin.ToString();
        for (int i = 0; i < maxCharacterCurrent; i++)
        {
            this.SpawnEnemy();
        }
        this.player.attackRoundObject.SetActive(false);
    }
    private void Update()
    {
        if (enemyCurrent.Count == 0) this.PostEvent(EventID.OnPlayerWin);
    }
    #region Register Listener 
    private void OnWeaponHitEnemy(OriginWeapon weapon)
    {
        if (enemyCurrent.Contains(weapon.victim)) this.enemyCurrent.Remove(weapon.victim);
        this.SpawnEnemyInGame();
        weapon.owner.AddPoint(weapon.victim.defeatPoint);
        weapon.owner.GrowthCharacter();
        weapon.owner.weaponImg.SetActive(true);
        weapon.victim.ChangeDeadState();
    }
    private void OnPlayerDie(OriginWeapon weapon)
    {
        weapon.victim.ChangeDeadState();
    }
    private void SpawnEnemyInGame()
    {
        if (countCharacter >= maxCharacter) return;
        if (enemyCurrent.Count < maxCharacterCurrent)
        {
            this.SpawnEnemy();
        }
    }

    private void OnEndGame()
    {
        this.player.OnInit();
        this.player.UpdateSaveData();
        this.player.wayPoint.UpdatePoint(player.point);
        countCharacter = 0;
        foreach(Character enemy in enemyCurrent)
        {
            enemy.wayPoint = null;
            enemy.nameUI = null;
        }
        enemyCurrent.Clear();
        SimplePool.CollectAll();
        for (int i = 0; i < maxCharacterCurrent; i++)
        {
            this.SpawnEnemy();
        }
    }
    private void OnStartGame()
    {
        this.SpawnWayPoint();
        this.SpawnNameBoard();
        this.TurnOnCirclePlayer();
        this.CheckGameNamePlayer();
    }
    private void TurnOnCirclePlayer()
    {
        player.attackRoundObject.SetActive(true);
    }
    private void CheckGameNamePlayer()
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
    }
    #endregion
    #region Spawn
    private Vector3 RandomPos()
    {
        Vector3 currentPosition = new(player.TF.position.x, 0f, player.TF.position.z);
        Vector3 randomPos = Random.insideUnitSphere * 7 * player.rangeAttack + currentPosition;
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
        countCharacter++;
        enemyCurrent.Add(bot);
        bot.skinColor.material = ColorManager.Instance.SetRandomColor();
        bot.TF.position = RandomPos();
        bot.OnInit();
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
    public void SpawnWayPoint()
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
    public  void SpawnNameBoard()
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
    #endregion
    public string GetNumAlive()
    {
        return (maxCharacter - (countCharacter - enemyCurrent.Count)).ToString();
    }
    public string GetRank()
    {
        return (maxCharacter - (countCharacter - enemyCurrent.Count) +1).ToString();
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
