using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] protected List<WeaponSO> weaponDatas;

    [SerializeField] protected OriginWeapon weaponPrefabs;

    private void Awake()
    {
        this.LoadWeaponData();
    }
    private void LoadWeaponData()
    {
        //if (this.weaponDatas.Count > 0) return;
        WeaponSO[] weaponDatasResource = Resources.LoadAll<WeaponSO>(Constant.pathWeaponData);
        foreach (WeaponSO weaponData in weaponDatasResource)
        {
            if (!weaponDatas.Contains(weaponData)) this.weaponDatas.Add(weaponData);
        }
    }
    public void SpawnWeapon(Character owner)
    {
        OriginWeapon weapon = SimplePool.Spawn<OriginWeapon>(weaponPrefabs);
        weapon.owner = owner;
        weapon.OnInit();
        weapon.transform.position = owner.transform.position + Vector3.up * 1f;
    }
    public void SpawnWeapon(Character owner,OriginWeapon prefab)
    {
        OriginWeapon weapon = SimplePool.Spawn<OriginWeapon>(prefab);
        weapon.owner = owner;
        weapon.OnInit();
        weapon.transform.position = owner.transform.position + Vector3.up * 1f;
    }
}
