using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private EnemyCharacter _enemyCharacter;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    public float stopDistance;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyCharacter = this.gameObject.GetComponent<EnemyCharacter>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        _navMeshAgent.SetDestination(_enemyCharacter.Target.transform.position);
        float remainingDistance = _navMeshAgent.remainingDistance;
        _navMeshAgent.stoppingDistance = stopDistance;

        if (remainingDistance > stopDistance)
        {
            _animator.SetBool("isWalking", true);
        }
        else 
        {
            _animator.SetBool("isWalking", false);
        }
    }

}
