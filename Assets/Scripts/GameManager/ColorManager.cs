using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class ColorManager : Singleton<ColorManager>
{
    public List<Material> listColor;
    public int currentColor = 0;
    public Material RandomColor()
    {
        Material material = new Material(Shader.Find(Constant.standard));
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        material.color = randomColor;
        return material;
    }
    public void AddMaterial()
    {
        for (int i = 0;  i < 20; i ++)
        {
            listColor.Add(RandomColor());
        }
    }
    public Material SetRandomColor()
    {
        Material color = listColor[currentColor];
        if (currentColor == listColor.Count - 1) currentColor = 0;
        else currentColor += 1;
        return color;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(ColorManager))]
public class ColorManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColorManager colorManager = (ColorManager)target;

        if (GUILayout.Button("Add Color List"))
        {
            colorManager.AddMaterial();
        }
    }
}
#endif
