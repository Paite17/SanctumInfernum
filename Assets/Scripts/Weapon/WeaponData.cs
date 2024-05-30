using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this enum is for identifying different types of weapon, which will affect its behaviour
public enum WeaponType
{
    RANGED_SINGLE_SHOT,
    RANGED_BURST,
    MELEE
}



// This class is for storing the data for a weapon

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
public class WeaponData : ScriptableObject
{
    // info
    [SerializeField] private string weaponName;

    // shooting
    [SerializeField] private float damagePerProjectile;
    [SerializeField] private float projectileMaxLifetime;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject projectileObj;

    // data
    [SerializeField] private float projectilesPerShot;
    [SerializeField] private bool randomBurstSpread;   // determines if a burst of projectiles would fire in a fixed or randomised spread.
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private float fireCooldownTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject weaponObject;

    public string WeaponName
    {
        get { return weaponName; }
    }

    public float DamagePerProjectile
    {
        get { return  damagePerProjectile; }
        set { damagePerProjectile = value; }
    }

    public float ProjectileMaxLifetime
    {
        get { return projectileMaxLifetime; }
    }

    public float ProjectilesPerShot
    {
        get { return projectilesPerShot; }
    }

    public bool RandomBurstSpread
    {
        get { return randomBurstSpread; }
        set { randomBurstSpread = value; }
    }

    public WeaponType _WeaponType
    {
        get { return _weaponType; }
    }

    public float FireCooldownTime
    {
        get { return  fireCooldownTime; }
    }

    public float FireRate
    {
        get { return fireRate; }
    }

    public GameObject ProjectileObj
    {
        get { return projectileObj; }
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }
}
