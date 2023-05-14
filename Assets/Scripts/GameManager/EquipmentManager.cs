using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EquipmentManager : Singleton<EquipmentManager>
{
    public List<EquipmentSO> weaponDatas;
    public List<EquipmentSO> pantDatas;
    public List<EquipmentSO> hatDatas;
    public List<EquipmentSO> shieldDatas;
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
        EquipmentSO[] weaponDatasResource = Resources.LoadAll<EquipmentSO>(Constant.pathWeaponData);
        foreach (EquipmentSO weaponData in weaponDatasResource)
        {
            this.weaponDatas.Add(weaponData);
        }
    }
    public void LoadHatData()
    {
        //container = GameObject.Find("WeaponPool").transform;
        hatDatas.Clear();
        EquipmentSO[] hatDatasResource = Resources.LoadAll<EquipmentSO>(Constant.pathHatData);
        foreach (EquipmentSO hatData in hatDatasResource)
        {
            this.hatDatas.Add(hatData);
        }
    }
    public void SpawnWeapon(Character character)
    {
        OriginWeapon weapon = SimplePool.Spawn<OriginWeapon>(character.typeWeapon);
        weapon.owner = character;
        weapon.OnInit();
        weapon.TF.position = character.TF.position + Vector3.up * 1f;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EquipmentManager))]
public class EquipmentManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EquipmentManager equipmentManager = (EquipmentManager)target;

        if (GUILayout.Button("Load Data"))
        {
            equipmentManager.LoadWeaponData();
            equipmentManager.LoadHatData();
        }
    }
}
#endif
