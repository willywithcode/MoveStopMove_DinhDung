using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    MainMenu,
    InGame,
    Pause,
    Question,
    ShopSkinMenu,
    ShopWeaponMenu,
    EndGame
}
public class GameManager : Singleton<GameManager>
{
    public GameState currentState;

    public int currentCoin;

    public void BuffCoin(int coin)
    {
        currentCoin += coin;
    }

}
