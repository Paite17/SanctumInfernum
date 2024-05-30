using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// whoever decided that separating health and movement into two separate scripts in this instance is a dumbass
public class EnemyMeleeHealth : MonoBehaviour
{
    [Header("Enemy Health Settings")]
    // integers are the spawn of satan, use floats for things like this
    public float enemyAgentHealth = 100;
    public EnemyFOVMelee EnemyFOV;

    public Animator anim;

    //private float despawnTimer;
    public AudioClip[] deathClips;
     
    public EnemySpawner enemySpawner;


    private bool attacked;
    private void Awake()
    {
        EnemyFOV = GetComponent<EnemyFOVMelee>();
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void enemyTakeDamage(int projDamage, int stunTime = 0)
    {
       
        enemyAgentHealth -= projDamage;

        if (EnemyFOV != null)
        {
            EnemyFOV.stunTime = stunTime;
            EnemyFOV.canSeePlayer = true;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            if (enemyAgentHealth > 0)
            {
                attacked = true;
                enemyAgentHealth -= collision.gameObject.GetComponent<Projectile>().ProjectileDamage;
                anim.SetBool("DamageTaken", attacked);
                StartCoroutine(Delay());
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MeleeAttack")
        {
            if (!attacked)
            {
                // gotta go through more hoops for melee damage (i think)
                enemyAgentHealth -= other.gameObject.GetComponentInParent<Weapon>().weaponData.DamagePerProjectile;
                attacked = true;
                StartCoroutine(Delay());
            }
            
        }
    }

    private void Death()
    {
        //enemySpawner.waves[enemySpawner.currentWaveIndex].enemiesLeft--;
        
        anim.SetTrigger("IsDying");
        Destroy(gameObject); // Destroy the enemy object after the explosion
        return;
    }

    public void Update()
    {
        // moving this to update for the moment
        if (enemyAgentHealth <= 0)
        {
            Death();
            //despawnTimer = 4;
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);

        attacked = false;
    }
}