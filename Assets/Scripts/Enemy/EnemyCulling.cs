using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCulling : MonoBehaviour
{
    [SerializeField] private float maxDistanceBeforeCulling;
    private EnemyFOVMelee meleeEnemy;
    private EnemyFOVAndShoot shootingEnemy;
    private NavMeshAgent agent;
    private PlayerMovement plr;
    private EnemyMeleeHealth meleeHealth;
    private EnemyRangedHealth rangeHealth;
    private float agentDistance;

    // Start is called before the first frame update
    void Start()
    {
        meleeEnemy = GetComponent<EnemyFOVMelee>();
        shootingEnemy = GetComponent<EnemyFOVAndShoot>();
        agent = GetComponent<NavMeshAgent>();
        plr = FindObjectOfType<PlayerMovement>();
        meleeHealth = GetComponent<EnemyMeleeHealth>();
        rangeHealth = GetComponent<EnemyRangedHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        agentDistance = Vector3.Distance(transform.position, plr.transform.position);
        //Debug.LogWarning("Distance = " + agentDistance);

        if (agentDistance > maxDistanceBeforeCulling)
        {
            if (meleeEnemy != null)
            {
                meleeEnemy.enabled = false;
            }

            if (shootingEnemy != null)
            {
                shootingEnemy.enabled = false;
            }

            if (meleeHealth != null)
            {
                meleeHealth.enabled = false;
            }

            if (rangeHealth != null)
            {
                rangeHealth.enabled = false;
            }

            if (agent != null)
            {
                agent.enabled = false;
            }
        }
        else
        {
            if (meleeEnemy != null)
            {
                meleeEnemy.enabled = true;
            }

            if (shootingEnemy != null)
            {
                shootingEnemy.enabled = true;
            }

            if (meleeHealth != null)
            {
                meleeHealth.enabled = true;
            }

            if (rangeHealth != null)
            {
                rangeHealth.enabled = true;
            }
       
            if (agent != null)
            {
                agent.enabled = true;
            }
        }
    }
}
