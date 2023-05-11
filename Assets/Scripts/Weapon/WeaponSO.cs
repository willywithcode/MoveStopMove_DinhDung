using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponSO : ScriptableObject
{
    public GameUnit weaponType;
    public OriginWeapon weaponCtrl;
    public GameObject weaponImg;
    public string description;
    public string nameWeapon;
    public string price;
    public Sprite imageInShop;
}
