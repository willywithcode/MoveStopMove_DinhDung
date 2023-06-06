using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class ButtonCtrl : MonoBehaviour
{
    public UnityAction<int,RectTransform> action;
    public int index;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Button button= GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        action?.Invoke(index,rectTransform);
    }
}
