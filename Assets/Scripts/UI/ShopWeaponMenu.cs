using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponMenu : MonoBehaviour
{
    public TextMeshProUGUI textMeshName;
    public TextMeshProUGUI textMeshDescription;
    public TextMeshProUGUI textMeshPrice;
    public Image weaponImage;

    public WeaponSO currentWeapon;
    public int indexWeapon;
    public int numWeapon;

    public PlayerCtrl playerCtrl;
    private void Start()
    {
        numWeapon = WeaponManager.Instance.weaponDatas.Count;
        Debug.Log(numWeapon);
        this.ChangeWeapon(WeaponManager.Instance.weaponDatas[0], 0);
    }
    public void ExitMenu()
    {
        UIManager.Instance.EnterStateUI(GameState.MainMenu);
    }
    public void ChangeNext()
    {
        if (indexWeapon == numWeapon - 1) this.ChangeWeapon(WeaponManager.Instance.weaponDatas[0], 0);
        else this.ChangeWeapon(WeaponManager.Instance.weaponDatas[indexWeapon + 1], indexWeapon + 1);
        //Debug.Log(1);
    }
    public void ChangePrevious()
    {
        if (indexWeapon == 0) this.ChangeWeapon(WeaponManager.Instance.weaponDatas[numWeapon - 1], numWeapon - 1);
        else this.ChangeWeapon(WeaponManager.Instance.weaponDatas[indexWeapon - 1], indexWeapon - 1);
        //Debug.Log(2);
    }
    public void ChangeWeapon(WeaponSO nextWeapon,int index)
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
        this.ExitMenu();
    }
}
