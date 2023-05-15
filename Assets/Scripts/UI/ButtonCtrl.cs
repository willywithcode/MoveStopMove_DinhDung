using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class ButtonCtrl : MonoBehaviour
{
    public UnityAction<int> action;
    public int index;
    private void Start()
    {
        Button button= GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        action?.Invoke(index);
    }
}
