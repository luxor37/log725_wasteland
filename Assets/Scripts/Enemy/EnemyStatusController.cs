using Player;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemyStatusController : GameEntity, IknockBack
    {
        private Animator _animator;

        private bool isHit;
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Knockback();
        }

        public int getCurrentHealth()
        {
            return this.currentHealth;
        }

        public void Knockback()
        {
            if (!isHit)
            {
                isHit = true;
                _animator.SetTrigger("isHit");
            }
        }

        public void ResetHit()
        {
            isHit = false;
        }
    }
}