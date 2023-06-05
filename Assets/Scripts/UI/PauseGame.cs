using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : BaseGameState
{
    [SerializeField] private Slider slider;
    private void Start()
    {
        slider.onValueChanged.AddListener(param => SoundManger.Instance.ChangeMasterVolume(param));
    }
    public void ExitGame()
    {
        UIManager.Instance.EnterStateUI(GameState.Question);
    }
    public void OnChangeVolume(float val)
    {
        slider.value = val;
    }
}
