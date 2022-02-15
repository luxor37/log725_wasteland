using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyCharacter : GameCharacter
    {
        private Animator _animator;
        public GameObject _target;

        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
        }

        public override int CalculateDamage()
        {
            return base.CalculateDamage();
        }

        public override void knockBack()
        {
            base.knockBack();
            _animator.SetTrigger("isHit");
        }

        public void TakeDamage(Attack atk)
        {
            //TODO : Build damage calculate system
            CalculateDamage();
            base.TakeDamage(atk.BasicAttack);
            knockBack();
            
        }

        public GameObject Target
        {
            get => _target;
            set => _target = value;
        }

        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }
        
        public void ResetHit()
        {
            _animator.ResetTrigger("isHit");
        }
    }
    
    
}