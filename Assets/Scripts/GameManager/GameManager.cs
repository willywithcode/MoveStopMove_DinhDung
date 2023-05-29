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
    public bool isHavingKilled;
    private string killer;
    private string victim;
    public List<int> listBoughtPantID;
    public List<int> listBoughtHatID;
    public List<int> listBoughtShieldID;
    public int currentCoin;
    public int currentHat;
    public int currentShield;  
    public int currentPant;
    public void AlermMassageKill(string kill, string killed)
    {
        isHavingKilled = true;
        killer = kill;
        victim = killed;
    }
    public string GetMessageKill()
    {
        return killer + Constant.killAlermMessage + victim;
    }
}
