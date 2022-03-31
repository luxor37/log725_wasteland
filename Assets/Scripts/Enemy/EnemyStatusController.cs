using Status;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyStatusController : StatusController
    {
        new void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _particlesController = GetComponent<ParticlesController>();
            _body = GetComponent<Rigidbody>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public new void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Knockback();
        }

        public bool getHit()
        {
            return this.isHit;
        }
    }
}