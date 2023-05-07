using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] protected GameObject weaponPrfab;
    [SerializeField] protected List<WeaponSO> weaponDatas;

    [SerializeField] protected ThrowWeapon weaponPrefabs;

    private void Awake()
    {
        this.LoadWeaponData();
    }
    private void LoadWeaponData()
    {
        if (this.weaponDatas.Count > 0) return;
        WeaponSO[] weaponDatas = Resources.FindObjectsOfTypeAll(typeof(WeaponSO)) as WeaponSO[];

        foreach (WeaponSO weaponData in weaponDatas)
        {
            this.weaponDatas.Add(weaponData);
        }
    }
    public void SpawnWeapon(Character owner)
    {
        ThrowWeapon weapon = SimplePool.Spawn<ThrowWeapon>(weaponPrefabs);
        weapon.owner = owner;
        weapon.OnInit();
        weapon.transform.position = owner.transform.position + Vector3.up * 1f;
    }
}
