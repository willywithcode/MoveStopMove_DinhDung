using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : BaseGameState
{
    [SerializeField] private TextMeshProUGUI txtCoinEarnUI;
    [SerializeField] private TextMeshProUGUI txtCoinRankUI;
    private int coinEarn;
    private void Awake()
    {
        this.RegisterListener(EventID.OnPlayerWin, (param) => OnPlayerWin());
        this.RegisterListener(EventID.OnPlayerDie, (param) => OnPlayerDie());
    }
    public override void GoMainMenu()
    {
        base.GoMainMenu();
        this.PostEvent(EventID.OnEndGame);
        GameManager.Instance.currentCoin += coinEarn;
        UIManager.Instance.txtCoinCurrent.text = GameManager.Instance.currentCoin.ToString();
        LevelManager.Instance.Player.ChangeState(LevelManager.Instance.Player.idle);
    }
    private void GetCoinEarn()
    {
        coinEarn = LevelManager.Instance.Player.point;
        txtCoinEarnUI.text = coinEarn.ToString();
    }
    private void OnPlayerWin()
    {
        this.GetCoinEarn();
        txtCoinRankUI.text = "1";
    }
    private void OnPlayerDie()
    {
        this.GetCoinEarn();
        txtCoinRankUI.text = LevelManager.Instance.GetRank();
    }
}
