using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class ButtonNavi : MonoBehaviour
{
    public UnityAction<GameObject> action;
    private void Start()
    {
        Button button =this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        action?.Invoke(this.gameObject);
    }
}
