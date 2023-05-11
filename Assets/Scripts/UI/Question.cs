using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : BaseGameState
{
    public void GoBackSetting()
    {
        UIManager.Instance.EnterStateUI(GameState.Pause);
    }
}
