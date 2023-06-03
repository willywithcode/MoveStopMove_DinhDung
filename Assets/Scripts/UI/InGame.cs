using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InGame : BaseGameState
{
    [SerializeField] private TextMeshProUGUI txtAliveUI;
    private void Awake()
    {
        this.RegisterListener(EventID.OnStartGame, (param) => ChangeAlive());
        this.RegisterListener(EventID.OnWeaponHitEnemy, (param) => ChangeAlive());
    }
    public void ChangeAlive()
    {
        txtAliveUI.text = LevelManager.Instance.GetNumAlive();
    }
    public void PauseGame()
    {
        UIManager.Instance.EnterStateUI(GameState.Pause);
    }
}
