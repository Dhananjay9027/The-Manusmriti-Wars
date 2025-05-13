using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIInterfaces;

public class EnemyMovement : IMovable
{
    private UnityEngine.AI.NavMeshAgent _agent;
    private float _walkPointRange;
    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private Animator _animator;

    public EnemyMovement(UnityEngine.AI.NavMeshAgent agent, float walkPointRange,Animator animator)
    {
        _agent = agent;
        _walkPointRange = walkPointRange;
        _animator = animator;
    }

    public void Patrol()
    {
       // Debug.Log(_agent.velocity.magnitude);
        _agent.speed = 1.2f;
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
        if (!_walkPointSet)
        {
            SearchWalkPoint();
        }
        if (_walkPointSet)
        {
            _agent.SetDestination(_walkPoint);
            Vector3 distanceToWalk = _agent.transform.position - _walkPoint;
            if (distanceToWalk.magnitude < 1f)
            {
                _walkPointSet = false;
            }
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(
            _agent.transform.position.x + randomX,
            _agent.transform.position.y,
            _agent.transform.position.z + randomZ
        );

        if (Physics.Raycast(_walkPoint, -_agent.transform.up, 2f))
        {
            _walkPointSet = true;
        }
    }

    public void Chase(Transform target)
    {
      //  Debug.Log(_agent.velocity.magnitude);
       // Debug.Log(_agent.velocity.x);
        _agent.speed = 4f;
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
        _agent.SetDestination(target.position);
    }
}
