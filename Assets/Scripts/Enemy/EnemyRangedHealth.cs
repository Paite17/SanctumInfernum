using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedHealth : MonoBehaviour
{
    public Animator anim;

    public float enemyRangedHealth = 100f;

    
    public EnemySpawner enemySpawner;

    private bool attacked;


    private void Start()
    {
        // this is not how you get the enemy spawner
        //enemySpawner = GetComponent<EnemySpawner>();

        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //Can change this value if needed to balance
            enemyRangedHealth -= collision.gameObject.GetComponent<Projectile>().ProjectileDamage;
            anim.SetBool("DamageTaken", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MeleeAttack")
        {
            if (!attacked)
            {
                // gotta go through more hoops for melee damage (i think)
                enemyRangedHealth -= other.gameObject.GetComponentInParent<Weapon>().weaponData.DamagePerProjectile;
                anim.SetBool("DamageTaken", true);
                attacked = true;
                StartCoroutine(Delay());
            }
        }
    }

    void Update()
    {
        if (enemyRangedHealth <= 0f)
        {
            //Debug.Log("ENEMY DIE PLEASE");
            //enemySpawner.waves[enemySpawner.currentWaveIndex].enemiesLeft--;
            StartCoroutine(Dying());
        }
    }

    private IEnumerator Dying()
    {
        Debug.LogWarning("RUN COROUTINE");
        anim.SetBool("IsDying", true);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        Destroy(this.gameObject);
    }


    // change a bool on a delay
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);

        attacked = false;
    }
}
