using Player;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemyCharacter : GameCharacter, IknockBack
    {
        private Animator _animator;
        public GameObject floatingPoint;

        private bool isHit;
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
        }

        public override void TakeDamage(int damage)
        {
            Instantiate(floatingPoint, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
            floatingPoint.GetComponentInChildren<TextMesh>().text = "-" + damage;
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