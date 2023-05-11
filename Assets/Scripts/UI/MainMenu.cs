using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   public void  StartGame()
    {
        UIManager.Instance.EnterStateUI(GameState.InGame);
    }
    public void WeaponShop()
    {
        UIManager.Instance.EnterStateUI(GameState.ShopWeaponMenu);

    }
    public void SkinShop()
    {
        UIManager.Instance.EnterStateUI(GameState.ShopSkinMenu);
    }
}
