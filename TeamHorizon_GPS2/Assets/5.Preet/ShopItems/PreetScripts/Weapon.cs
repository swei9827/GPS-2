using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int weaponID;

    public Sprite unboughtSprite;
    public Sprite boughtSprite;

    public float weaponPrice;
    public bool bought;

}
