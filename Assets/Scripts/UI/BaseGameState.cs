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
}
