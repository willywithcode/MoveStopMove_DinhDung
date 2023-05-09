using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] protected List<WeaponSO> weaponDatas;

    [SerializeField] protected ThrowWeapon weaponPrefabs;
    [SerializeField] private GameObject prefabs;

    private void Awake()
    {
        this.LoadWeaponData();
        this.LoadWeapon();
    }
    private void LoadWeapon()
    {
        for (int i = 0; i < weaponDatas.Count; i++)
        {
            SimplePool.Preload(weaponDatas[i].weaponType, 15, this.transform, false, false);
        }
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
        ThrowWeapon weapon = SimplePool.Spawn<ThrowWeapon>(weaponPrefabs);
        //ThrowWeapon weapon = Instantiate(prefabs).GetComponent<ThrowWeapon>();
        weapon.owner = owner;
        weapon.OnInit();
        weapon.transform.position = owner.transform.position + Vector3.up * 1f;
    }
}
