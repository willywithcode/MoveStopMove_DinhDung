using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinMenu : BaseGameState
{

    public GameObject prefabButton;

    public Transform contentHatShop;
    public Transform contentPantShop;
    public Transform contentShieldShop;

    public PlayerCtrl player;
    private void Start()
    {

        this.LoadShopHatUI();
        this.LoadShopPantUI();
        this.LoadShopShieldUI();
    }
    private void LoadShopHatUI()
    {
        int count = EquipmentManager.Instance.hatDatas.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject myButton = Instantiate(prefabButton, contentHatShop);
            myButton.GetComponent<ButtonCrl>().index = i;
            myButton.GetComponent<Image>().sprite = EquipmentManager.Instance.hatDatas[i].imageInShop;
            myButton.GetComponent<Button>().onClick.AddListener(delegate { ChangeHat(myButton.GetComponent<ButtonCrl>().index); });
        }

    }
    private void LoadShopPantUI()
    {
        int count = EquipmentManager.Instance.pantDatas.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject myButton = Instantiate(prefabButton, contentPantShop);
            myButton.GetComponent<ButtonCrl>().index = i;
            myButton.GetComponent<Image>().sprite = EquipmentManager.Instance.pantDatas[i].imageInShop;
            myButton.GetComponent<Button>().onClick.AddListener(delegate { ChangePant(myButton.GetComponent<ButtonCrl>().index); });
        }

    }
    private void LoadShopShieldUI()
    {
        int count = EquipmentManager.Instance.shieldDatas.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject myButton = Instantiate(prefabButton, contentShieldShop);
            myButton.GetComponent<ButtonCrl>().index = i;
            myButton.GetComponent<Image>().sprite = EquipmentManager.Instance.shieldDatas[i].imageInShop;
            myButton.GetComponent<Button>().onClick.AddListener(delegate { ChangeShield(myButton.GetComponent<ButtonCrl>().index); });
        }

    }
    public void ChangeHat(int index)
    {
        if (player.hat != null) Destroy(player.hat);
        player.hat = Instantiate(EquipmentManager.Instance.hatDatas[index].weaponImg, player.hatContainer);
    }
    public void ChangePant(int index)
    {
        Debug.Log(index);
    }
    public void ChangeShield(int index)
    {
        Debug.Log(index);
    }
}
