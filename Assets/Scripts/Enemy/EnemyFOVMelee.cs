using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


// making this for the boss, which wont have patrol points
public enum EnemyType
{
    ENEMY,
    BOSS
}

public class EnemyFOVMelee : MonoBehaviour
{
    [Header("FOV Settings")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public bool chasePlayer;

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
    public bool inAttackRange;
    public float lostInterestForSec;

    public int stunTime;
    private int currentPP;
    public AudioClip[] shootClips;
    public EnemyType enemyType;

    [SerializeField] private Animator anim;

    // audio shit
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip attack1;
    private void Start()
    {
        // PUT PLAYER REF HERE!!
        playerRef = PlayerMovement.GetReference.gameObject;
        playerPos = PlayerMovement.GetReference.transform;

        anim = GetComponentInChildren<Animator>();
        //audioSource = GetComponent<AudioSource>();

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
            if (patrolPoints.Length > 0)
            {
                if (Vector3.Distance(transform.position, patrolPoints[currentPP].position) < 1)
                {
                    currentPP = (currentPP + 1) % patrolPoints.Length;
                }

                enemyAgent.SetDestination(patrolPoints[currentPP].position);

                anim.SetBool("IsWalking", true);

                yield return null;
            }
            else
            {
                yield return null;
            }
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
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }

        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    public void Update()
    {
        if (canSeePlayer)
        {
            hasSeenPlayer = true;
            lostInterestForSec = loseInterestAtSec;
            Vector3 playerPosY = playerPos.position;
            playerPosY.y = enemyAgentTransform.position.y;
            enemyAgentTransform.LookAt(playerPosY);

        }
        else if (!canSeePlayer)
        {
            lostInterestForSec -= Time.deltaTime;
            if (lostInterestForSec <= 0f)
            {
                // could be used for Wwise for detecting when the player leaves combat???
                // (Maybe not actually since it'd need to check for all enemies)

                hasSeenPlayer = false;
                playerPos.GetComponent<PlayerMovement>().beingAttacked = false;
                anim.SetBool("IsRunning", false);
            }

        }

        if (hasSeenPlayer)
        {
            if (enemyAgent.isActiveAndEnabled) enemyAgent.isStopped = false;

            if (chasePlayer != false)
            {
                AkSoundEngine.PostEvent("Player_Seen", gameObject);
                // could use this for Wwise for detecting when to start combat music

                enemyAgent.SetDestination(playerPos.position);

                // activate boss UI if its a boss
                if (enemyType == EnemyType.BOSS)
                {
                    FindObjectOfType<UIScript>().ShowBossHealthBar();
                }

                playerPos.GetComponent<PlayerMovement>().beingAttacked = true;

                anim.SetBool("IsRunning", true);
            }

        }
        else if (!hasSeenPlayer && enemyType == EnemyType.ENEMY)
        {
            if (patrolPoints.Length > 0)
            {
                StartCoroutine(PatrolRoutine());
            }
        }
    }

    private void FixedUpdate()
    {
        stunTime = stunTime > 0 ? stunTime - 1 : 0;
    }
}