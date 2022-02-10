
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public Transform attackPoint;
        public Vector3 attackRange = new Vector3(1,1,1);
        public LayerMask enemyLayers;
        public int attack;
        
        private bool attacking = false;
        private int AttackIndex;

        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            _animator = this.gameObject.GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G) )
            {
                Attack();
            }
        }
        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private void Hit()
        {
            Collider[] hitEnemies = Physics.OverlapBox(attackPoint.position, attackRange,Quaternion.identity, enemyLayers);
            foreach (Collider enemies in hitEnemies)
            {
                Debug.Log(enemies.name);
                IDamageble damagebleable = enemies.GetComponent<IDamageble>();
                if(damagebleable != null)
                    damagebleable.TakeDamage(attack);
            }
        }

        private void OnDrawGizmos()
        {
            if (attackPoint == null)
                return;
            Gizmos.DrawWireCube(attackPoint.position, attackRange);
        }
    }
}
