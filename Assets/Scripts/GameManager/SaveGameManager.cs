using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
    public PlayerCtrl player;
    //string for testing
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
        return SaveGameManager.SAVE_3;
    }
    public virtual void LoadSaveGame()
    {
        string savedJson = PlayerPrefs.GetString(this.GetSaveName());
        SaveData savedData = JsonUtility.FromJson<SaveData>(savedJson);
        if (!string.IsNullOrEmpty(savedData.namePlayer)) player.namePlayer = savedData.namePlayer;
        if (savedData.dataPantID.Count >0) GameManager.Instance.listBoughtPantID = savedData.dataPantID;
        if (savedData.dataShieldID.Count > 0) GameManager.Instance.listBoughtShieldID = savedData.dataShieldID;
        if (savedData.dataHatID.Count > 0) GameManager.Instance.listBoughtHatID = savedData.dataHatID;
        if (savedData.dataCoin != null) GameManager.Instance.currentCoin = savedData.dataCoin;
    }
    public virtual void SaveGame()
    {
        SaveData saveData = new SaveData();
        saveData.dataPantID  = GameManager.Instance.listBoughtPantID;
        saveData.dataShieldID   = GameManager.Instance.listBoughtShieldID;
        saveData.dataHatID   = GameManager.Instance.listBoughtHatID;
        saveData.namePlayer = player.namePlayer;
        saveData.dataCoin = GameManager.Instance.currentCoin;
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(this.GetSaveName(), json);
    }
}
class SaveData
{
    public List<int> dataPantID;
    public List<int> dataHatID;
    public List<int> dataShieldID;
    public string namePlayer;
    public int dataCoin;
}

