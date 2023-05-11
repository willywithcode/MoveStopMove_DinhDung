using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BaseGameState
{
    public void PauseGame()
    {
        UIManager.Instance.EnterStateUI(GameState.Pause);
    }
}
