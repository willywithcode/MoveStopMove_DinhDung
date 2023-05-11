using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseGameState
{
 
    public void WeaponShop()
    {
        UIManager.Instance.EnterStateUI(GameState.ShopWeaponMenu);

    }
    public void SkinShop()
    {
        UIManager.Instance.EnterStateUI(GameState.ShopSkinMenu);
    }
}
