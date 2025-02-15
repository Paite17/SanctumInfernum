using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// QUICK NOTE:
// COMMENTING OUT ALL THE SETDESTINATION() CALLS BECAUSE RN IT CAUSES ERRRORS, UNCOMMENT THEM WHEN YOU WORK ON THAT STUFF
// -Lewis
public class EnemyFOVAndShoot : MonoBehaviour
{
    [Header("FOV Settings")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public bool chasePlayer;

    [Header("Projectile Settings")]
    [SerializeField] private float shootCooldown = 3f;
    public float projSpeed;

    [Header("GameObj + Transform Ref")]
    public GameObject playerRef;
    public Transform playerPos;
    public GameObject enemyProj;
    public Transform projSpawnPoint;
    public Transform enemyAgentTransform;

    [Header("NavMesh + LayerMask Settings")]
    public NavMeshAgent enemyAgent;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public float loseInterestAtSec = 4f;
    public Transform[] patrolPoints;

    [Header("Debugging Tools for FOV")]
    [SerializeField] public bool canSeePlayer;
    public bool hasSeenPlayer;
    public bool inShootRange;
    public float lostInterestForSec;
    private float bulletTime;

    public int stunTime;
    private int currentPP;
    public AudioClip[] shootClips;

    public EnemyType enemyType;

   
    Animator anim;

    // audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attack;
    private void Start()
    {
        playerRef = PlayerMovement.GetReference.gameObject;
        playerPos = PlayerMovement.GetReference.transform;

        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();

        hasSeenPlayer = false;
        enemyAgentTransform = transform;

        StartCoroutine(FOVRoutine());

        if (enemyType == EnemyType.ENEMY)
        {
            StartCoroutine(PatrolRoutine());
        }
        

        if (chasePlayer is false)
        {
            enemyAgent = GetComponent<NavMeshAgent>();
            enemyAgent.enabled = !enemyAgent.enabled;
        }
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private IEnumerator PatrolRoutine()
    {

        while (!hasSeenPlayer)
        {
            if (Vector3.Distance(transform.position, patrolPoints[currentPP].position) < 1)
            {
                currentPP = (currentPP + 1) % patrolPoints.Length;
            }

            if (enemyAgent.enabled)
            {
                enemyAgent.SetDestination(patrolPoints[currentPP].position);
            }
            

            anim.SetBool("IsWalking", true);

            yield return null;
        }
    }

    public void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    inShootRange = true;
                }
                else
                {
                    canSeePlayer = false;
                    inShootRange = false;
                }
            }
            else
            {
                canSeePlayer = false;
                inShootRange = false;
            }

        }
        else if (canSeePlayer)
            canSeePlayer = false;
        playerPos.GetComponent<PlayerMovement>().beingAttacked = false;
    }

    void ShootAtPlayer()
    {
        if (stunTime > 0) return;
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0) return;
        bulletTime = shootCooldown;

        //GlobalSpeaker.GetReference.Play(shootClips[UnityEngine.Random.Range(0, shootClips.Length)]);

        /*audioSource.clip = attack;
        audioSource.Play(); */
        GameObject enemyProjectile = Instantiate(enemyProj, projSpawnPoint.transform.position, projSpawnPoint.transform.rotation) as GameObject;
        Rigidbody projRbody = enemyProjectile.GetComponent<Rigidbody>();
        projRbody.AddForce(projRbody.transform.forward * projSpeed);
        Destroy(enemyProjectile, 5f);
    }

    public void Update()
    {
        if (canSeePlayer)
        {
            hasSeenPlayer = true;
            lostInterestForSec = loseInterestAtSec;
            projSpawnPoint.transform.LookAt(playerPos.transform.position);
            Vector3 playerPosY = playerPos.position;
            playerPosY.y = enemyAgentTransform.position.y;
            enemyAgentTransform.LookAt(playerPosY);

            if (inShootRange)
                ShootAtPlayer();
            anim.SetBool("AttackPlayer", true);
        }
        else
        {
            lostInterestForSec -= Time.deltaTime;
            if (lostInterestForSec <= 0f)
            {
                hasSeenPlayer = false;
                anim.SetBool("IsRunning", false);
            }

        }

        if (hasSeenPlayer)
        {
            if (enemyAgent.isActiveAndEnabled) enemyAgent.isStopped = false;

            if (chasePlayer != false)
            {
                enemyAgent.SetDestination(playerPos.position);
                anim.SetBool("IsRunning", true);
            }

            playerPos.GetComponent<PlayerMovement>().beingAttacked = true;

        }
        else
        {
            if (enemyAgent.isActiveAndEnabled) enemyAgent.isStopped = true;
        }
    }

    private void FixedUpdate()
    {
        stunTime = stunTime > 0 ? stunTime - 1 : 0;
    }
}
