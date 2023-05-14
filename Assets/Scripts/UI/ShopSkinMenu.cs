using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinMenu : BaseGameState
{

    public GameObject prefabButton;

    public Transform contentHatShop;

    public PlayerCtrl player;
    private void Start()
    {

        this.LoadShopHatUI();
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
    public void ChangeHat(int index)
    {
        if (player.hat != null) Destroy(player.hat);
        player.hat = Instantiate(EquipmentManager.Instance.hatDatas[index].weaponImg, player.hatContainer);
    }
}
