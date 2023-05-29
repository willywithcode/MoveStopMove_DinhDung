using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
public enum StateShopSkin
{
    Hat,
    Pant,
    Shield,
    FullSet
}
public class ShopSkinMenu : BaseGameState
{
    public StateShopSkin currentStateSkin;
    public EquipmentSO currentEquipment;

    public GameObject prefabButton;
    public TextMeshProUGUI textMeshPrice;
    public TextMeshProUGUI textMeshDescription;


    public Transform contentHatShop;
    public Transform contentPantShop;
    public Transform contentShieldShop;

    private Dictionary<GameObject,Image> buttonViews = new Dictionary<GameObject,Image>();
    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> views = new List<GameObject>();
    public PlayerCtrl player;

    GameObject tempHat;
    GameObject tempShield;
    int currenrtIndex;
    private void Start()
    {
        this.AddDictButtonView();
        this.LoadShopHatUI();
        this.LoadShopPantUI();
        this.LoadShopShieldUI();
        currentStateSkin = StateShopSkin.Hat;
    }
    private void AddDictButtonView()
    {
        foreach (GameObject button in buttons)
        {
            buttonViews.Add(button, button.GetComponent<Image>());
            button.GetComponent<ButtonNavi>().action += CheckOnClickButton;
        }
    }
    public override void GoMainMenu()
    {
        base.GoMainMenu();
        this.ClearChoices();
        player.ChangeAnim(Constant.ANIM_IDLE);
    }
    public void CheckOnClickButton(GameObject button)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            this.ClearChoices();
            if (buttons[i] == button)
            {
                buttonViews[buttons[i]].enabled = true;
                views[i].SetActive(true);
                currentStateSkin = (StateShopSkin)i;
            }
            else
            {
                buttonViews[buttons[i]].enabled = false;
                views[i].SetActive(false);
            }
        }
    }
    private void ClearChoices()
    {
        player.pantType.material = player.pantCurrent;
        if (player.hatCurrent != null) player.hatCurrent.SetActive(true);
        Destroy(this.tempHat);
        if (player.shieldCurrent != null) player.shieldCurrent.SetActive(true);
        Destroy(this.tempShield);
    }
    public void ChooseItem()
    {
        if (String.Equals(textMeshPrice.text, Constant.selectStringBtn))
        {
            this.ChangeItem();
            return;
        }
        if (GameManager.Instance.currentCoin >= int.Parse(textMeshPrice.text))
        {
            this.ChangeItem();
            int lossCoin = int.Parse(textMeshPrice.text);
            GameManager.Instance.currentCoin -= lossCoin;
            UIManager.Instance.UpdateCoinCurrent();
            this.AddDataID();
            this.ChangeButtonSellect();
        }
    }
    private void AddDataID() 
    {
        switch (currentStateSkin)
        {
            case StateShopSkin.Hat:
                GameManager.Instance.listBoughtHatID.Add(currenrtIndex);
                break;
            case StateShopSkin.Pant:
                GameManager.Instance.listBoughtPantID.Add(currenrtIndex);
                break;
            case StateShopSkin.Shield:
                GameManager.Instance.listBoughtShieldID.Add(currenrtIndex);
                break;
            case StateShopSkin.FullSet:
                break;
        }
    }
    public void ChangeItem ()
    {
        if (player.pantCurrent != player.pantType.material) player.pantCurrent = player.pantType.material;
        if (player.hatCurrent != null && tempHat != null) Destroy(player.hatCurrent);
        if (tempHat != null)
        {
            player.hatCurrent = Instantiate(tempHat, player.hatContainer);
            Destroy(tempHat);
        }
        if (player.shieldCurrent != null && tempShield != null) Destroy(player.shieldCurrent);
        if (tempShield != null)
        {
            player.shieldCurrent = Instantiate(tempShield, player.shieldContainer);
            Destroy(tempShield);
        }
        this.UpdateItem();
    }
    private void UpdateItem()
    {
        switch (currentStateSkin) 
        {
            case StateShopSkin.Hat:
                player.rangeTempHat = currentEquipment.attackRange;
                break;
            case StateShopSkin.Pant:
                player.speedTempPant = currentEquipment.speed;
                break;
            case StateShopSkin.Shield:
                break;
            case StateShopSkin.FullSet:
                break;
        }
        player.speed = 5 + player.speedTempPant;
        player.rangeAttack = 5 + player.rangeTempHat + player.rangeTempWeapon;
        player.rangeCtrl.ChangeAttackRange(player.rangeAttack);
    }

    private void LoadShopHatUI()
    {
        int count = EquipmentManager.Instance.hatDatas.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject myButton = Instantiate(prefabButton, contentHatShop);
            myButton.GetComponent<ButtonCtrl>().index = i;
            myButton.GetComponent<Image>().sprite = EquipmentManager.Instance.hatDatas[i].imageInShop;
            myButton.GetComponent<ButtonCtrl>().action += ChangeHat;
        }

    }
    private void LoadShopPantUI()
    {
        int count = EquipmentManager.Instance.pantDatas.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject myButton = Instantiate(prefabButton, contentPantShop);
            myButton.GetComponent<ButtonCtrl>().index = i;
            myButton.GetComponent<Image>().sprite = EquipmentManager.Instance.pantDatas[i].imageInShop;
            myButton.GetComponent<ButtonCtrl>().action += ChangePant;

        }

    }
    private void LoadShopShieldUI()
    {
        int count = EquipmentManager.Instance.shieldDatas.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject myButton = Instantiate(prefabButton, contentShieldShop);
            myButton.GetComponent<ButtonCtrl>().index = i;
            myButton.GetComponent<Image>().sprite = EquipmentManager.Instance.shieldDatas[i].imageInShop;
            myButton.GetComponent<ButtonCtrl>().action += ChangeShield;
        }

    }
    public void ChangeHat(int index)
    {
        currenrtIndex = index;
        if (player.hatCurrent != null) player.hatCurrent.SetActive(false);
        if(tempHat != null) Destroy(tempHat);
        tempHat = Instantiate(EquipmentManager.Instance.hatDatas[index].weaponImg, player.hatContainer);
        if (GameManager.Instance.listBoughtHatID.Contains(index)) this.ChangeButtonSellect();
        else this.ChangeDescription(index, EquipmentManager.Instance.hatDatas);
    }
    public void ChangePant(int index)
    {
        currenrtIndex = index;
        player.pantType.material = EquipmentManager.Instance.pantDatas[index].material;
        if (GameManager.Instance.listBoughtPantID.Contains(index)) this.ChangeButtonSellect();  
        else this.ChangeDescription(index, EquipmentManager.Instance.pantDatas);

    }
    public void ChangeShield(int index)
    {
        currenrtIndex = index;
        if (player.shieldCurrent != null) player.shieldCurrent.SetActive(false);
        if (tempShield != null) Destroy(tempShield);
        tempShield = Instantiate(EquipmentManager.Instance.shieldDatas[index].weaponImg, player.shieldContainer);
        if (GameManager.Instance.listBoughtShieldID.Contains(index)) this.ChangeButtonSellect();
        else this.ChangeDescription(index, EquipmentManager.Instance.shieldDatas);
    }
    private void ChangeDescription(int index,List<EquipmentSO> list)
    {
        textMeshPrice.text = list[index].price;
        textMeshDescription.text = list[index].description;
        currentEquipment = list[index];
    }
    private void ChangeButtonSellect()
    {
        textMeshPrice.text = Constant.selectStringBtn;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ShopSkinMenu))]
public class ShopSkinMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ShopSkinMenu fillCanvas = (ShopSkinMenu)target;

        if (GUILayout.Button("Fill"))
        {

        }
    }
}
#endif
