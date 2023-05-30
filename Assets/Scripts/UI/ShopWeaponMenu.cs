using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponMenu : BaseGameState
{
    public TextMeshProUGUI textMeshName;
    public TextMeshProUGUI textMeshDescription;
    public TextMeshProUGUI textMeshPrice;
    public Image weaponImage;

    public EquipmentSO currentWeapon;
    public int indexWeapon;
    public int numWeapon;

    public PlayerCtrl playerCtrl;
    private void Start()
    {
        numWeapon = EquipmentManager.Instance.weaponDatas.Count;
        this.ChangeWeapon(EquipmentManager.Instance.weaponDatas[0], 0);
    }
    public void ChangeNext()
    {
        if (indexWeapon == numWeapon - 1) this.ChangeWeapon(EquipmentManager.Instance.weaponDatas[0], 0);
        else this.ChangeWeapon(EquipmentManager.Instance.weaponDatas[indexWeapon + 1], indexWeapon + 1);
    }
    public void ChangePrevious()
    {
        if (indexWeapon == 0) this.ChangeWeapon(EquipmentManager.Instance.weaponDatas[numWeapon - 1], numWeapon - 1);
        else this.ChangeWeapon(EquipmentManager.Instance.weaponDatas[indexWeapon - 1], indexWeapon - 1);
    }
    public void ChangeWeapon(EquipmentSO nextWeapon, int index)
    {
        if (nextWeapon == currentWeapon) return;
        indexWeapon = index;
        currentWeapon = nextWeapon;
        textMeshName.text = nextWeapon.name;
        textMeshDescription.text = nextWeapon.description;
        weaponImage.sprite = nextWeapon.imageInShop;
        if (SaveGameManager.Instance.listBoughtWeaponID.Contains(indexWeapon))
        {
            this.ChangeSelectBtn();
            return;
        }
        textMeshPrice.text = nextWeapon.price;
    }
    public void SelectWeapon()
    {
        if (string.Equals(textMeshPrice.text,Constant.selectStringBtn))
        {
            this.ChangeWeaponPlayer();
            return;
        }
        if (GameManager.Instance.currentCoin >= int.Parse(textMeshPrice.text))
        {
            GameManager.Instance.currentCoin -= int.Parse(textMeshPrice.text);
            this.ChangeWeaponPlayer();
            this.ChangeSelectBtn();
            SaveGameManager.Instance.listBoughtWeaponID.Add(indexWeapon);
        }
    }
    private void ChangeWeaponPlayer()
    {
        SaveGameManager.Instance.currentWeapon = indexWeapon +1;
        this.playerCtrl.typeWeapon = currentWeapon.weaponType.poolType;
        this.playerCtrl.AssignWeapon();
        playerCtrl.rangeTempWeapon = currentWeapon.attackRange;
        playerCtrl.rangeAttack = Constant.foudationAttackRange + (playerCtrl.rangeTempWeapon + playerCtrl.rangeTempHat) * 0.1f;
        playerCtrl.rangeCtrl.ChangeAttackRange(playerCtrl.rangeAttack);
        this.GoMainMenu();
    }
    private void ChangeSelectBtn()
    {
        textMeshPrice.text = Constant.selectStringBtn;
    }
}
