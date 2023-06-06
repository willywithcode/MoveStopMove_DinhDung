using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiftCode : BaseGameState
{
    [SerializeField] private TMP_InputField giftCodeString;

    public override void GoMainMenu()
    {
        base.GoMainMenu();
        this.gameObject.SetActive(false);
        UIManager.Instance.TurnOffDesplaySetting();
    }
    public  void CheckGiftCode()
    {
        string tmp = giftCodeString.text.Trim();
        if (tmp.Equals(Constant.giftCode1)) GameManager.Instance.BuffCoin(100000);
        else if (giftCodeString.text.Equals(Constant.giftCode2)) GameManager.Instance.BuffCoin(100);
        else if (giftCodeString.text.Equals(Constant.giftCode3)) GameManager.Instance.BuffCoin(100);
        this.GoMainMenu();
        UIManager.Instance.UpdateCoinCurrent();
    }
}
