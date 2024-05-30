using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject meleeHurtBox;
    [SerializeField] private Animator animator;

    public bool cooldownActive;

    [SerializeField] private float timeSinceLastShot;

    // good god
    private bool CanAttack() => timeSinceLastShot > 1f / (weaponData.FireRate / 60f);

    public float cooldownTimer;

    public bool shooting;

    private Camera cam;

    public List<WeaponData> tempWeaponList;

    [SerializeField] private float xAimSensitivity;
    [SerializeField] private float yAimSensitivity;
    private float yRotation;
    private float xRotation;

    [SerializeField] private GameObject shotgunModel;
    [SerializeField] private GameObject knifeModel;


    
    // Start is called before the first frame update
    void Start()
    {
        
        SwitchWeapon(weaponData);

        // MAKE SURE THAT WEAPONDATA IS UPDATED WITH WHATEVER IS EQUIPPED
        PlayerAttack.attackInput += Attack;

        // make sure this is getting the right camera
        cam = Camera.main;

        // this is for debugging remove it later
        /*
        var weapon = Resources.LoadAll("Weapons", typeof(WeaponData)).Cast<WeaponData>();
        foreach (var current in weapon)
        {
            tempWeaponList.Add(current);
        } */

        // get the knife as a default
        tempWeaponList.Add(weaponData);

        // this is terrible
        // hack fix for a problem where entering the dungeon scene from slums breaks the weapon system
        Scene scene = SceneManager.GetActiveScene();
        
        // this sucks but there's a bug
        GameObject player = GameObject.Find("Player");
        GameObject fuck = GameObject.Find("WeaponShoot");
        GameObject fuck2 = GameObject.Find("Melee Detection");
        firePos = fuck.transform;
        if (meleeHurtBox == null)
        {
            meleeHurtBox = fuck2;
            Instantiate(meleeHurtBox, player.transform);
        }
        
        Instantiate(firePos, player.transform);
        firePos.gameObject.SetActive(true);

       

        // end slums moment
        if (SceneManager.GetActiveScene().name == "EndSlums")
        {
            WeaponData shotgun = Resources.Load("Weapons/shotgun") as WeaponData;
            tempWeaponList.Add(shotgun);
        }

        Debug.Log(firePos);

    }

    // Update is called once per frame
    void Update()
    {


        timeSinceLastShot += Time.deltaTime;
        //Debug.Log(CanAttack());

        // debug inputs
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(tempWeaponList[0]); 
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (tempWeaponList[1] != null)
            {
                SwitchWeapon(tempWeaponList[1]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (tempWeaponList[2] != null)
            {
                SwitchWeapon(tempWeaponList[2]);
            }
        }


        // terrible rotation for aiming
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xAimSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * yAimSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        // rotate aiming obj
        firePos.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // sort out animations
        UpdateWeaponAnimationState();


    }

    // a more uniform way of switching weapon that can be reused in the actual game with UI
    public void SwitchWeapon(WeaponData weapon)
    {
        weaponData = weapon;
    }

    private void UpdateWeaponAnimationState()
    {
        switch (weaponData._WeaponType)
        {
            case WeaponType.RANGED_SINGLE_SHOT:
                shotgunModel.SetActive(false);
                knifeModel.SetActive(false);
                animator.SetBool("ThrowTypeEquiped", true);
                animator.SetBool("SwordTypeEquiped", false);
                animator.SetBool("GunTypeEquiped", false);
                break;
            case WeaponType.RANGED_BURST:
                shotgunModel.SetActive(true);
                knifeModel.SetActive(false);
                animator.SetBool("ThrowTypeEquiped", false);
                animator.SetBool("SwordTypeEquiped", false);
                animator.SetBool("GunTypeEquiped", true);
                break;
            case WeaponType.MELEE:
                shotgunModel.SetActive(false);
                knifeModel.SetActive(true);
                animator.SetBool("ThrowTypeEquiped", false);
                animator.SetBool("SwordTypeEquiped", true);
                animator.SetBool("GunTypeEquiped", false);
                break;
        }
    }

    public void Attack()
    {
        Debug.Log("Attack() called");
        //Shotgun_Fire.Post(gameObject);

        // make sure weapon is equipped?????
        shooting = true;

        // check weapon type
        switch (weaponData._WeaponType)
        {
            case WeaponType.RANGED_SINGLE_SHOT:
                if (CanAttack())
                {
                    // create the weapon's projectile at the shoot spot
                    // maybe give it a bit of a velocity too in the direction they're looking
                    GameObject temp = Instantiate(weaponData.ProjectileObj, firePos.position, Quaternion.identity);
                    // add velocity
                    temp.GetComponent<Projectile>().SetProjectile(cam.transform.forward, weaponData);
                    timeSinceLastShot = 0;
                }
                break;
            case WeaponType.RANGED_BURST:
                if (CanAttack())
                {
                    // do the same as the other one but with a bunch of projectiles
                    for (int i = 0; i < weaponData.ProjectilesPerShot; i++)
                    {
                        float x = 0;
                        float y = 0;
                        float z = 0;
                        if (weaponData.RandomBurstSpread)
                        {
                            x = Random.Range(0, 1.4f);
                            y = Random.Range(0, 1.2f);
                            z = Random.Range(0, 1.4f);
                        }
                        GameObject temp = null;
                        if (firePos != null)
                        {
                            temp = Instantiate(weaponData.ProjectileObj, firePos.position, Quaternion.identity);
                        }
                        
                        if (temp != null)
                        {
                            if (Random.Range(0, 1) == 0)
                            {
                                temp.transform.position = new Vector3(temp.transform.position.x + x, temp.transform.position.y + y, temp.transform.position.z + z);
                            }
                            else
                            {
                                temp.transform.position = new Vector3(temp.transform.position.x - x, temp.transform.position.y - y, temp.transform.position.z - z);
                            }
                            temp.GetComponent<Projectile>().SetProjectile(cam.transform.forward, weaponData);
                        }
                    }


                    timeSinceLastShot = 0;
                }
                break;
            case WeaponType.MELEE:
                Debug.Log("Melee weapon");

                if (CanAttack() && meleeHurtBox != null)
                {
                    Debug.Log("CanAttack is true");
                    StartCoroutine(MeleeAttack());
                }               
                break;
        }
        
    }


    private IEnumerator MeleeAttack()
    {

        Debug.Log("MELEE ATTACK!!!");

        meleeHurtBox.SetActive(true);
        
        
        // replace the number with a different value at some point
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


        meleeHurtBox.SetActive(false);
     
        
    }
}
