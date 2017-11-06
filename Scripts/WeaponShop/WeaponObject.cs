using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponObject : ScriptableObject
{

    

    public string weaponName = "Weapon1";       // Allows you to name each weapon object on information useful to the player. 
    public int cost = 50;
    public string description;

    public float fireRate = .5f;
    public int damage = 10;
    public float range = 100;
}
