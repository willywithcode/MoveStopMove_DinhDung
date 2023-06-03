using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : BaseGameState
{
    [SerializeField] private GameUnit prefab;
    public void GoBackSetting()
    {
        UIManager.Instance.EnterStateUI(GameState.Pause);
    }
    public override void GoMainMenu()
    {

        base.GoMainMenu();
        SimplePool.CollectAll();
        LevelManager.Instance.countCharacterCurrent= 0;
        LevelManager.Instance.enemyCurrent.Clear();
    }
}
