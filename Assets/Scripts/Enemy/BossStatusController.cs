using Status;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BossStatusController : StatusController
    {
        new void Start()
        {
            base.Start();
            Animator = GetComponent<Animator>();
            ParticlesController = GetComponent<ParticlesController>();
            Body = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
        }

        public new void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }
    }
}