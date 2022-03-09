using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public GameObject _target;
    private Animator _animator;

    public float stopDistance = 2f;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        _navMeshAgent.SetDestination(_target.transform.position);
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player"){
            var statusCtrl = other.gameObject.GetComponent<Status.StatusHandler>();
            statusCtrl.TakeDamage(100);
        }
    }
}
