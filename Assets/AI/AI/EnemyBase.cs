using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform player;
    protected bool playerInSightRange, playerInAttackRange;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public abstract void HandleAI();
}
