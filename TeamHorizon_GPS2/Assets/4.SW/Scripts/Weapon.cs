using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [Tooltip("Weapon Damage")]
    public int damage;

    [Tooltip("Weapon Range, recommend value 500 - infinite")]
    public float range;

    //[Tooltip("Health value between 0 and 100.")]
    //public int currentAmmo;

    [Tooltip("Maximum ammo")]
    public int maxAmmo;

    [Tooltip("Times require for a reload")]
    public float reloadTime;

    [Tooltip("Is this weapon reload in clip")]
    public bool clipReload;

    [Tooltip("If weapon reloading bullet one by one, define a reload time for each bullet cost(in seconds)")]
    public float eachBulletRequire;

    [Tooltip("Effect while bullet hit")]
    public GameObject effect;

    //[Tooltip("Health value between 0 and 100.")]
    //public bool reloading;
}
