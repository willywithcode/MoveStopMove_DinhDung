using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] protected List<WeaponSO> weaponDatas;
    [SerializeField] protected OriginWeapon weaponPrefabs;
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
            SimplePool.Preload(weaponDatas[i].weaponType, 25, this.transform, false, false);
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
    public void SpawnWeapon(Character character)
    {
        OriginWeapon weapon = SimplePool.Spawn<OriginWeapon>(character.typeWeapon);
        //ThrowWeapon weapon = Instantiate(prefabs).GetComponent<ThrowWeapon>();
        Debug.Log(character.typeWeapon);
        weapon.owner = character;
        weapon.OnInit();
        weapon.transform.position = character.transform.position + Vector3.up * 1f;
    }
}
