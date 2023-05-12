using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WeaponManager : Singleton<WeaponManager>
{
    public List<WeaponSO> weaponDatas;
    public Transform container;

    private void Awake()
    {
        this.LoadWeapon();
    }
    private void LoadWeapon()
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            SimplePool.Preload(weaponDatas[i].weaponType, 25, container, false, false);
        }
    }
    public void LoadWeaponData()
    {
        container = GameObject.Find("WeaponPool").transform;
        weaponDatas.Clear();
        WeaponSO[] weaponDatasResource = Resources.LoadAll<WeaponSO>(Constant.pathWeaponData);
        foreach (WeaponSO weaponData in weaponDatasResource)
        {
            if (!weaponDatas.Contains(weaponData)) this.weaponDatas.Add(weaponData);
        }
    }
    public void SpawnWeapon(Character character)
    {
        OriginWeapon weapon = SimplePool.Spawn<OriginWeapon>(character.typeWeapon);
        weapon.owner = character;
        weapon.OnInit();
        weapon.transform.position = character.transform.position + Vector3.up * 1f;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WeaponManager))]
public class WeaponManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WeaponManager weaponManager = (WeaponManager)target;

        if (GUILayout.Button("Load Weapon Data"))
        {
            weaponManager.LoadWeaponData();
        }
    }
}
#endif
