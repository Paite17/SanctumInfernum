using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponData weaponToPickUp;

    public Weapon weaponRef;

    public void PickUpWeapon()
    {
        weaponRef.tempWeaponList.Add(weaponToPickUp);

        // dont ask
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
