using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float existTime;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileSpeed;

    private float existingTimer;

    private Vector3 direction;

    public float ProjectileDamage
    {
        get { return projectileDamage; } 
        set { projectileDamage = value; }
    }

    // particle stuff should probably go here later

    // Start is called before the first frame update
    void Awake()
    {
        // ignore player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<SphereCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        existingTimer += Time.deltaTime;

        if (existingTimer > existTime)
        {
            Destroy(gameObject);
        }

        rb.AddForce(direction.normalized * projectileSpeed, ForceMode.Force);
    }

    // called from elsewhere
    public void SetProjectile(Vector3 direction_, WeaponData weaponStats)
    {
        // Projectile lifetime and speed are already assigned for the enemies, hence a separate script for
        // enemy projectiles will be made.
        existTime = weaponStats.ProjectileMaxLifetime;
        projectileDamage = weaponStats.DamagePerProjectile;
        projectileSpeed = weaponStats.ProjectileSpeed;
        direction = direction_;
        rb.AddForce(direction.normalized * projectileSpeed, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
        
    }
}
