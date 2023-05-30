using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillMessge : MonoBehaviour
{
    private float timeDestroy;
    private float timeCount;
    public UnityAction killUI;
    private void Start()
    {
        timeDestroy = 1;
        timeCount = 0; 
    }
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeDestroy) this.OnAreaDestroy();
    }
    private void OnAreaDestroy()
    {
        timeCount= 0;
        this.gameObject.SetActive(false);
        PopupManager.Instance.isHavingKilled = false;
    }
}
