using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameState : MonoBehaviour
{
    public virtual void EnterInGame()
    {
        UIManager.Instance.EnterStateUI(GameState.InGame);
    }
    public virtual void GoMainMenu()
    {
        UIManager.Instance.EnterStateUI(GameState.MainMenu);
    }
    public virtual void TurnOffCoinDesplay()
    {
        UIManager.Instance.coinDesplayContainer.SetActive(false);
    }
    public virtual void TurnOnCoinDesplay()
    {
        UIManager.Instance.coinDesplayContainer.SetActive(true);
    }
    public virtual void TurnOnCirclePlayer()
    {
        LevelManager.Instance.Player.attackRoundObject.SetActive(true) ;
    }
    public virtual void TurnOffCirclePlayer()
    {
        LevelManager.Instance.Player.attackRoundObject.SetActive(false);
    }
}
