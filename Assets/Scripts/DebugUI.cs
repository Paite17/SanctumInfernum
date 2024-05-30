using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TMP_Text debugWeaponName;
    [SerializeField] private Weapon playerWeapon;
    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = FindObjectOfType<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        debugWeaponName.text = playerWeapon.weaponData.WeaponName;
    }
}
