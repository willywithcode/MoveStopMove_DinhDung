using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Dictionary<GameState,Canvas> dictState = new Dictionary<GameState, Canvas>();

    public Canvas mainMenu;
    public Canvas inGame;
    public Canvas shopSkinMenu;
    public Canvas shopWeaponMenu;
    public Canvas endGame;
    public Canvas pauseGame;
    public Canvas question;
    private void Awake()
    {
        this.AddStates();
        this.AwakeState();
    }
    
    public void EnterStateUI(GameState nextGameState)
    {
        if (GameManager.Instance.currentState == nextGameState) return;
        GameManager.Instance.currentState = nextGameState;
        foreach (var state in dictState)
        {
            if (state.Key == GameManager.Instance.currentState) state.Value.enabled = true;
            else state.Value.enabled = false;
        }
    }
    public void AwakeState()
    {
        GameManager.Instance.currentState = GameState.MainMenu;
        foreach (var state in dictState)
        {
            if (state.Key == GameManager.Instance.currentState) state.Value.enabled = true;
            else state.Value.enabled = false;
        }
    }
    private void AddStates()
    {
        dictState.Add(GameState.MainMenu, mainMenu);
        dictState.Add(GameState.InGame, inGame);
        dictState.Add(GameState.ShopSkinMenu, shopSkinMenu);
        dictState.Add(GameState.ShopWeaponMenu, shopWeaponMenu);
        dictState.Add(GameState.Pause, pauseGame);
        dictState.Add(GameState.EndGame, endGame);
        dictState.Add(GameState.Question, question);
    }
}
