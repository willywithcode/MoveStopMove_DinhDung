using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : Singleton<SoundManger>
{
    [SerializeField] private AudioSource musicSource, effectSource;
    [SerializeField] private List<AudioClip> growthCharacterEffect;
    [SerializeField] private AudioClip effectClickButton;
    [SerializeField] private AudioClip startGameEffect;
    [SerializeField] private AudioClip loseGameEffect;
    [SerializeField] private AudioClip winGameEffect;
    [SerializeField] private List<AudioClip> weaponHitEffect;
    [SerializeField] private List<AudioClip> deadCharacterEffect;
    private void Start()
    {
        this.RegisterListener(EventID.OnStartGame,(param) => OnStartGame());
        this.RegisterListener(EventID.OnWeaponHitEnemy, (param) => OnWeaponHit());
        this.RegisterListener(EventID.OnPlayerDie, (param) => OnLoseGame());
        this.RegisterListener(EventID.OnPlayerWin, (param) => OnWinGame());
    }
    public void PlayEffect(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    #region Register Listener
    public void OnStartGame()
    {
        musicSource.Stop();
        effectSource.PlayOneShot(startGameEffect);

    }
    public void OnWeaponHit()
    {
        effectSource.PlayOneShot(weaponHitEffect[Random.Range(0, weaponHitEffect.Count)]);
        effectSource.PlayOneShot(deadCharacterEffect[Random.Range(0,deadCharacterEffect.Count)]);
    }
    public void OnLoseGame()
    {
        effectSource.PlayOneShot(weaponHitEffect[Random.Range(0, weaponHitEffect.Count)]);
        effectSource.PlayOneShot(loseGameEffect);
    }
    public void OnWinGame()
    {
        effectSource.PlayOneShot(winGameEffect);
    }
    #endregion
    public void EffectClickBtn()
    {
        effectSource.PlayOneShot(effectClickButton);
    }
    public void TurnOnMusicMainMenu()
    {
        musicSource.Play();
    }
    public void TurnEffectGrowthCharacter()
    {
        effectSource.PlayOneShot(growthCharacterEffect[Random.Range(0,growthCharacterEffect.Count)]);
    }
}
