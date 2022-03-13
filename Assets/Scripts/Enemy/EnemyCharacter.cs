using Player;
using UnityEditor;
using UnityEngine;


    public enum EnemyType
    {
        Zombie, Ryder
    }
    public class EnemyCharacter : GameCharacter, IknockBack
    {
        public int EnemyID;
        private Animator _animator;
        public GameObject floatingPoint;

        private bool isHit;
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(Attack atk)
        {
            showFloatingDamage(atk.BasicAttack);
            base.TakeDamage(atk.BasicAttack);
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

        private void showFloatingDamage(int damage)
        {
            if (floatingPoint != null)
            {
                Instantiate(floatingPoint, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
                floatingPoint.GetComponentInChildren<TextMesh>().text = "-" + damage;
            }
        }

        public void ResetHit()
        {
            isHit = false;
        }
    }
