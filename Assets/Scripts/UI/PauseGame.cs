using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : BaseGameState
{
    public void ExitGame()
    {
        UIManager.Instance.EnterStateUI(GameState.Question);
    }
}
