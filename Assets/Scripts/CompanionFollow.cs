using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField]
    private Transform target, companion;
    [SerializeField]
    private NavMeshAgent companionAgent;

    private void Update()
    {
        Vector3 targetPosY = target.position;
        targetPosY.y = companion.position.y;
        companion.LookAt(targetPosY);
        companionAgent.SetDestination(target.position);
    }
}
