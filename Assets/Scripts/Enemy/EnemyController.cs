using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject _target;
    private Animator _animator;

    public float StopDistance = 2f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        Move();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void Move()
    {
        var target = new Vector3(_target.transform.position.x, _target.transform.position.y, 0);
        _navMeshAgent.SetDestination(target);
        var remainingDistance = _navMeshAgent.remainingDistance;
        _navMeshAgent.stoppingDistance = StopDistance;

        if (remainingDistance > StopDistance)
        {
            _animator.SetBool("isWalking", true);
        }
        else 
        {
            _animator.SetBool("isWalking", false);
        }
    }
}
