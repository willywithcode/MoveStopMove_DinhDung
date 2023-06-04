using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftCtrl : MonoBehaviour
{
    Transform TF;
    private void Start()
    {
        this.TF = transform;
    }
    private void OnEnable()
    {
        
    }
    private void Update()
    {
        if(TF.position.y >= 0)
        {
            TF.position += Vector3.down * 2 * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.CHARACTER) || other.CompareTag(Constant.PLAYER)) {
            this.gameObject.SetActive(false);
            LevelManager.Instance.countTimeGift = 0;
        }
    }
}
