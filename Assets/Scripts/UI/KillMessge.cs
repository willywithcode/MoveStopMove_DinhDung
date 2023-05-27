using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMessge : MonoBehaviour
{
    private float timeDestroy;
    private float timeCount;
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
        GameManager.Instance.isHavingKilled = false;
    }
}
