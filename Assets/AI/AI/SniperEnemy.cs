using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIInterfaces;

public class SniperEnemy : EnemyBase
{
    [SerializeField] private Transform BulletPrefab;
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private Transform player;
    [SerializeField] private float sightRange, attackRange;
    public LayerMask whatIsGround, WhatisPlayer;
    public float timeBetweenAttacks;
    private IAttackable _attackBehavior;
    private bool Isattacked = false;
    protected override void Awake()
    {
        base.Awake();
        _attackBehavior = new EnemyAttack(SpawnPosition, BulletPrefab, timeBetweenAttacks, player);
    }

    private void Update()
    {
        HandleAI();
    }
    public override void HandleAI()
    {
        // Sniper-specific logic
        if (!Isattacked)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatisPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatisPlayer);
        }

        if (playerInSightRange)
        {
            _attackBehavior.Attack(player);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            playerInSightRange = true;
            Isattacked = true;
        }
    }
}
