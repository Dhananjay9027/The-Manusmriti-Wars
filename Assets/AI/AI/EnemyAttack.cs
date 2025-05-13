using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIInterfaces;

public class EnemyAttack : IAttackable
{
    private Transform _spawnPosition;
    private Transform _bulletPrefab;
    private float _timeBetweenAttacks;
    private bool _alreadyAttacked;
    private Transform _player;
    public EnemyAttack(Transform spawnPosition, Transform bulletPrefab, float timeBetweenAttacks,Transform Player)
    {
        _spawnPosition = spawnPosition;
        _bulletPrefab = bulletPrefab;
        _timeBetweenAttacks = timeBetweenAttacks;
        _player = Player;
    }

    public void Attack(Transform target)
    {
        if (_alreadyAttacked) return;

        float randomYOffset = Random.Range(0.5f, 0.7f); // Random range for Y-axis
        float randomZOffset = Random.Range(0.0f, 0.2f); // Random range for Z-axis
        Vector3 targetPosition = _player.transform.position + new Vector3(0, randomYOffset, randomZOffset);
        Vector3 aimDir = (targetPosition - _spawnPosition.position).normalized;
        Object.Instantiate(_bulletPrefab, _spawnPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        _alreadyAttacked = true;
        Timer.ResetAfter(() => _alreadyAttacked = false, _timeBetweenAttacks);
    }


}
