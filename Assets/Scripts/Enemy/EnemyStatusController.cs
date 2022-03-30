using Status;
using UnityEngine;

namespace Enemy
{
    public class EnemyStatusController : StatusController
    {
        new void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _particlesController = GetComponent<ParticlesController>();
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