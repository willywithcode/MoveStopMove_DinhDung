using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InGame : BaseGameState
{
    public TextMeshProUGUI killAlerm;
    public GameObject killAlermArea;
    private void Update()
    {
        if (GameManager.Instance.isHavingKilled == true)
        {
            killAlermArea.SetActive(true);
            this.killAlerm.text = GameManager.Instance.GetMessageKill();
        }
    }
    public void PauseGame()
    {
        UIManager.Instance.EnterStateUI(GameState.Pause);
    }
}
