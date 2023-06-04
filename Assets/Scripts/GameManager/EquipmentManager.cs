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
    private void Start()
    {
        this.RegisterListener(EventID.OnWeaponHitEnemy, (param) => OnWeaponHitEnemy((OriginWeapon)param));
        this.RegisterListener(EventID.OnPlayerDie, (param) => OnWeaponHitEnemy((OriginWeapon)param));
    }
    private void LoadWeapon()
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            SimplePool.Preload(weaponDatas[i].weaponType, 30, container, true, false);
        }
    }
    public void OnWeaponHitEnemy(OriginWeapon weapon)
    {
        weapon.EndAttack();
    }
    #region Load Data in Editor
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
        hatDatas.Clear();
        EquipmentSO[] hatDatasResource = Resources.LoadAll<EquipmentSO>(Constant.pathHatData);
        foreach (EquipmentSO hatData in hatDatasResource)
        {
            this.hatDatas.Add(hatData);
        }
    }
    public void LoadPantData()
    {
        pantDatas.Clear();
        EquipmentSO[] pantDatasResource = Resources.LoadAll<EquipmentSO>(Constant.pathPantData);
        foreach (EquipmentSO pantData in pantDatasResource)
        {
            this.pantDatas.Add(pantData);
        }
    }
    public void LoadShieldData()
    {
        shieldDatas.Clear();
        EquipmentSO[] shieldDatasResource = Resources.LoadAll<EquipmentSO>(Constant.pathShieldData);
        foreach (EquipmentSO shieldData in shieldDatasResource)
        {
            this.shieldDatas.Add(shieldData);
        }
    }

    #endregion
    public void SpawnWeapon(Character character)
    {
        OriginWeapon weapon = SimplePool.Spawn<OriginWeapon>(character.typeWeapon);
        weapon.owner = character;
        weapon.OnInit();
        weapon.TF.position = character.TF.position + Vector3.up * 1f;
    }
}
#region Editor
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
            equipmentManager.LoadPantData();
            equipmentManager.LoadShieldData();
        }
    }
}
#endif
#endregion