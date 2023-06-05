using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : BaseGameState
{
    public PlayerCtrl player;
    public TextMeshProUGUI nameString;
    public GameObject inputName;

    private void Update()
    {
        if (string.IsNullOrEmpty(player.namePlayer))
        {
            inputName.SetActive(true);
            return;
        }
        inputName.SetActive(false);
    }
    public void WeaponShop()
    {
        UIManager.Instance.EnterStateUI(GameState.ShopWeaponMenu);

    }
    public void SkinShop()
    {
        UIManager.Instance.EnterStateUI(GameState.ShopSkinMenu);
        player.ChangeAnim(Constant.ANIM_DanceShop);
    }
    public void EnterNamePlayer(string name)
    {
        player.namePlayer = nameString.text;
    }
    public override void EnterInGame()
    {
        base.EnterInGame();
        this.PostEvent(EventID.OnStartGame);
    }
    public void TurnOnSetting()
    {
        UIManager.Instance.TurnOnDesplaySetting();
    }
}
