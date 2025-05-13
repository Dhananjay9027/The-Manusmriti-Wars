using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIInterfaces;

public class NormalEnemy : EnemyBase
{
    private IMovable _movementBehavior;
    private IAttackable _attackBehavior;

    [SerializeField] private Transform BulletPrefab;
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private float WalkPointRange;
    [SerializeField] private float timeBetweenAttacks;
    private float sightRange = 20f;
    private float attackRange = 12f;
    [SerializeField] private Transform player;
    private Animator animator;

    private bool Isattacked = false;
    public LayerMask whatIsGround, WhatisPlayer;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        _movementBehavior = new EnemyMovement(agent, WalkPointRange,animator);
        _attackBehavior = new EnemyAttack(SpawnPosition, BulletPrefab, timeBetweenAttacks,player);
    }


    public override void HandleAI()
    {
        if (!Isattacked)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatisPlayer);
        }
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatisPlayer);
        if (!playerInSightRange && !playerInAttackRange)
        {
          //  animator.SetFloat("Speed", 2f);
            _movementBehavior.Patrol();
            
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            //  animator.SetFloat("Speed", 6f);
            transform.LookAt(player);
            _movementBehavior.Chase(player);
          
        }
        if (playerInAttackRange && playerInSightRange)
        {
            transform.LookAt(player);
          //  animator.SetLayerWeight(1, 0.5f);
         //   animator.SetFloat("Speed", 6f);
            _attackBehavior.Attack(player);
          
        }


    }
    private void Update()
    {
        if (agent.velocity.x < -0.1f)
        {
            animator.SetBool("Reverse",true);
            Debug.Log("neg h");
        }
        else
        {
            animator.SetBool("Reverse", false);
            Debug.Log("pos h");
        }
        if (agent.velocity.magnitude < 0.3)
        {
            // Debug.Log("slaw h BC");
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        HandleAI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            playerInSightRange = true;
            Isattacked = true;
          //  sightRange = 1000f;
           // playerInAttackRange = true;
        }
    }
}
