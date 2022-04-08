using Player;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject _target;
    private Animator _animator;

    public float stopDistance = 2f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        move();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void move()
    {
        var target = new Vector3(_target.transform.position.x, _target.transform.position.y, 0);
        _navMeshAgent.SetDestination(target);
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
        if (other.tag == "Character"){
            var statusCtrl = other.GetComponent<PlayerStatusController>();
            statusCtrl.TakeDamage(100);
        }
    }
}
