using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameState : MonoBehaviour
{
    public void EnterInGame()
    {
        UIManager.Instance.EnterStateUI(GameState.InGame);
    }
    public void GoMainMenu()
    {
        UIManager.Instance.EnterStateUI(GameState.MainMenu);
    }
}
