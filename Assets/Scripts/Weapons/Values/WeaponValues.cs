using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WeaponID
{
    Sword = 1,
}

[Serializable]
public class WeaponKeyValuePair
{
    public string key;
    public float value;
}

[CreateAssetMenu(fileName = "Weapon values", menuName = "Scriptable Objects/Weapon Values")]
public class WeaponValues : ScriptableObject
{
    public WeaponID weaponID;
    public float basePrepareAttackTime;
    public float baseEndAttackTime;
    public float baseDamage;
    public float baseReloadTime;
    public List<WeaponKeyValuePair> keyValuePairs;

    public float? GetWeaponValue(string key)
    {
        return keyValuePairs.FirstOrDefault((x) => (x.key == key))?.value;
    }
}