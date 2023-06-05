using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GiftCtrl : MonoBehaviour
{
    Transform TF;
    private float count;
    private float existTime = 30;
    private void Start()
    {
        this.TF = transform;
    }
    private void OnEnable()
    {
        count = 0;
    }
    private void Update()
    {
        if(TF.position.y >= 0 && GameManager.Instance.currentState != GameState.Pause)
        {
            TF.position += Vector3.down * 2 * Time.deltaTime;
        }
        count += Time.deltaTime;
        if(count >= existTime)
        {
            this.gameObject.SetActive(false);
            LevelManager.Instance.SpawnGift();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.CHARACTER) || other.CompareTag(Constant.PLAYER)) {
            this.gameObject.SetActive(false);
            LevelManager.Instance.countTimeGift = 0;
            Character character = Cache.GetScript(other);
            character.isUlti = true;
            if (other.CompareTag(Constant.PLAYER)) SoundManger.Instance.TurnEffectGrowthCharacter();
        }
    }
}
