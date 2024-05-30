using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeRange : MonoBehaviour
{
    private HashSet<Collider> objectsInside = new();
    //[SerializeField] private UIHealthBar pHealth;
    [SerializeField] private float hitCooldown = 0f;
    public const float DEF_COOLDOWN = 1.5f;
    private bool inRange = false;

    // why was this never made before
    [SerializeField] private float attackDamage;

    public Animator anim;
    private int attackHash;
    private void Start()
    {
        //anim = GetComponentInChildren<Animator>();
        
        attackHash = Animator.StringToHash("Attack");
    }

    private void Update()
    {
        foreach (Collider obj in objectsInside)
        {
            if (hitCooldown <= 0 && obj.CompareTag("Player"))
            {
                // PUT PLAYER TAKING DAMAGE CODE HERE!!
                obj.GetComponent<UIHealthBar>().TakeDamage(attackDamage);
                hitCooldown = DEF_COOLDOWN;
                anim.SetBool("AttackPlayer", true); 
                
            }

        }
        
        if (inRange)
        {
            hitCooldown -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectsInside.Add(other);
            inRange = true;

            
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectsInside.Remove(other);
            inRange = false;
            hitCooldown = DEF_COOLDOWN;

            // Set the "AttackPlayer" boolean to false when the player exits the trigger zone
            anim.SetBool("AttackPlayer", false);
            anim.SetBool("IsRunning", true);
        }
    }
}
