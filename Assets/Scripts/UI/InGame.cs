using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InGame : BaseGameState
{
    public TextMeshProUGUI killAlermTxt;
    public GameObject killAlermArea;
    public KillMessge killMessageUI;
    private void Update()
    {
        if (PopupManager.Instance.isHavingKilled == true)
        {
            killAlermArea.SetActive(true);
            this.killAlermTxt.text = PopupManager.Instance.GetMessageKill();
        }
    }
    public void PauseGame()
    {
        UIManager.Instance.EnterStateUI(GameState.Pause);
    }
}
