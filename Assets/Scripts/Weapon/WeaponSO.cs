using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponSO : ScriptableObject
{
    public GameUnit weaponType;
    public OriginWeapon weaponCtrl;
}
