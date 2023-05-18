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
    public void ChangeWeapon(EquipmentSO nextWeapon,int index)
    {
        if (nextWeapon == currentWeapon) return;
        currentWeapon = nextWeapon;
        indexWeapon = index;
        textMeshName.text = nextWeapon.name;
        textMeshDescription.text = nextWeapon.description; 
        textMeshPrice.text = nextWeapon.price;
        weaponImage.sprite = nextWeapon.imageInShop;
    }
    public void SelectWeapon()
    {
        this.playerCtrl.typeWeapon = currentWeapon.weaponType.poolType;
        this.playerCtrl.AssignWeapon();
        playerCtrl.rangeTempWeapon = currentWeapon.attackRange;
        playerCtrl.rangeAttack = 5 + playerCtrl.rangeTempWeapon + playerCtrl.rangeTempHat;
        playerCtrl.rangeCtrl.ChangeAttackRange(playerCtrl.rangeAttack);
        this.GoMainMenu();
    }
}
