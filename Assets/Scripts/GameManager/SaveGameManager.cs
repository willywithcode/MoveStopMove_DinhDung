using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
    [SerializeField] PlayerCtrl player;
    private const string SAVE_1 = "save_1";
    private const string SAVE_2 = "save_2";
    private const string SAVE_3 = "save_3";
    private void Start()
    {
        this.LoadSaveGame();
    }
    private void OnApplicationQuit()
    {
        this.SaveGame();
    }
    protected virtual string GetSaveName()
    {
        return SaveGameManager.SAVE_2;
    }
    public virtual void LoadSaveGame()
    {
        player.namePlayer = PlayerPrefs.GetString(this.GetSaveName());
    }
    public virtual void SaveGame()
    {
        string stringSave = player.namePlayer;
        PlayerPrefs.SetString(this.GetSaveName(), stringSave);
    }
}
