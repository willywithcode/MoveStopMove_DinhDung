using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private List<Sprite> imagesMusic;
    [SerializeField] private List<Sprite> imagesEffect;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button effectBtn;

    private void Start()
    {
        slider.onValueChanged.AddListener(param => SoundManger.Instance.ChangeMasterVolume(param));
    }
    public void Toggle(bool check)
    {
        if(check)
        {
            SoundManger.Instance.ToggleMusic();
            if(SoundManger.Instance.CheckMusicMute()) musicBtn.image.sprite = imagesMusic[1];
            else musicBtn.image.sprite = imagesMusic[0];
            return;
        }
        SoundManger.Instance.ToggleEffect();
        if (SoundManger.Instance.CheckEffectMute()) effectBtn.image.sprite = imagesEffect[1];
        else effectBtn.image.sprite = imagesEffect[0];
    }
    public void TurnOffSetting()
    {
        UIManager.Instance.TurnOffDesplaySetting();
    }
    public void OnChangeVolume(float val)
    {
        slider.value = val;
    }
    public void TurnOnGiftCode()
    {
        UIManager.Instance.giftcode.SetActive(true);
    }
}
