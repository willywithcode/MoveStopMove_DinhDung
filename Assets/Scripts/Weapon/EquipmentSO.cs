using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Weapon", menuName = "ScriptableObjects/EquipmentData", order = 1)]
public class EquipmentSO : ScriptableObject
{
    public GameUnit weaponType;
    public OriginWeapon weaponCtrl;
    public GameObject weaponImg;
    public string description;
    public string nameWeapon;
    public string price;
    public Sprite imageInShop;
    public Material material;
    public float speed;
    public float attackRange;
}
