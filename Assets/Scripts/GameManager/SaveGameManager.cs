using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
    public PlayerCtrl player;

    public List<int> listBoughtPantID;
    public List<int> listBoughtHatID;
    public List<int> listBoughtShieldID;
    public List<int> listBoughtWeaponID;
    public int currentHat;
    public int currentShield;
    public int currentPant;
    public int currentWeapon;
    //string for testing
    private const string SAVE_1 = "save_1";
    private const string SAVE_2 = "save_2";
    private const string SAVE_3 = "save_3";
    private void Awake()
    {
        this.LoadSaveGame();
    }
    private void OnApplicationQuit()
    {
        this.SaveGame();
    }
    protected virtual string GetSaveName()
    {
        return Constant.saveGame;
    }
    public virtual void LoadSaveGame()
    {
        string savedJson = PlayerPrefs.GetString(this.GetSaveName());
        SaveData savedData = JsonUtility.FromJson<SaveData>(savedJson);
        if (!string.IsNullOrEmpty(savedData.namePlayer)) player.namePlayer = savedData.namePlayer;
        if (savedData.dataPantID.Count > 0) listBoughtPantID = savedData.dataPantID;
        if (savedData.dataShieldID.Count > 0) listBoughtShieldID = savedData.dataShieldID;
        if (savedData.dataHatID.Count > 0) listBoughtHatID = savedData.dataHatID;
        if (savedData.dataWeaponID.Count > 0) listBoughtWeaponID = savedData.dataWeaponID;
        if (!listBoughtWeaponID.Contains(0)) listBoughtWeaponID.Add(0);
        if (savedData.dataCoin != 0) GameManager.Instance.currentCoin = savedData.dataCoin;
        if (savedData.currentHatID != 0) currentHat = savedData.currentHatID;
        if (savedData.currentPantID != 0) currentPant = savedData.currentPantID;
        if (savedData.currentShieldID != 0) currentShield = savedData.currentShieldID;
        if (savedData.currentWeaponID != 0) currentWeapon = savedData.currentWeaponID;
    }
    public virtual void SaveGame()
    {
        SaveData saveData = new SaveData();
        saveData.dataPantID = listBoughtPantID;
        saveData.dataShieldID = listBoughtShieldID;
        saveData.dataHatID = listBoughtHatID;
        saveData.dataWeaponID = listBoughtWeaponID;
        saveData.namePlayer = player.namePlayer;
        saveData.dataCoin = GameManager.Instance.currentCoin;
        saveData.currentHatID = currentHat;
        saveData.currentPantID = currentPant;
        saveData.currentShieldID = currentShield;
        saveData.currentWeaponID = currentWeapon;
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(this.GetSaveName(), json);
    }
}
class SaveData
{
    public List<int> dataPantID;
    public List<int> dataHatID;
    public List<int> dataShieldID;
    public List<int> dataWeaponID;
    public string namePlayer;
    public int dataCoin;
    public int currentHatID;
    public int currentShieldID;
    public int currentPantID;
    public int currentWeaponID;
}

