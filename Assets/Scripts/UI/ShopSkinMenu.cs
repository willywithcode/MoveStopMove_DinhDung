using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShopSkinMenu : BaseGameState
{

    public GameObject prefabButton;

    public Transform contentHatShop;
    public Transform contentPantShop;
    public Transform contentShieldShop;

    private  Dictionary<GameObject,Image> buttonViews = new Dictionary<GameObject,Image>();
    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> views = new List<GameObject>();
    public PlayerCtrl player;
    private void Start()
    {
        this.AddDictButtonView();
        this.LoadShopHatUI();
        this.LoadShopPantUI();
        this.LoadShopShieldUI();
    }
    private void AddDictButtonView()
    {
        foreach (GameObject button in buttons)
        {
            buttonViews.Add(button, button.GetComponent<Image>());
            button.GetComponent<ButtonNavi>().action += CheckOnClickButton;
        }
    }
    public void CheckOnClickButton(GameObject button)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == button)
            {
                buttonViews[buttons[i]].enabled = true;
                views[i].SetActive(true);
            }
            else
            {
                buttonViews[buttons[i]].enabled = false;
                views[i].SetActive(false);
            }
        }
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
