using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public GameObject _target;
    private Animator _animator;

    public float stopDistance = 2f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

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
        if (other.tag == "Player"){
            var statusCtrl = other.gameObject.GetComponent<Status.StatusController>();
            statusCtrl.TakeDamage(100);
        }
    }
}
