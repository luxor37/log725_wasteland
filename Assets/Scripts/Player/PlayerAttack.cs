using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public Transform attackPoint;
        public Vector3 attackRange = new Vector3(1,1,1);
        public LayerMask enemyLayers;
        public int attack = 10;
        
        private bool attacking = false;
        private float timeToAttack = 0.25f;
        private float timer = 0f;

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
            if (Input.GetKeyDown(KeyCode.G))
            {
                if(!attacking)
                    Attack();
            }
            if (attacking)
            {
                timer += Time.deltaTime;
                if (timer >= timeToAttack)
                {
                    attacking = false;
                    //attackArea.SetActive(attacking);
                    _animator.SetBool("isAttacking",attacking);
                    timer = 0f;
                }
            }
        }

        private void Attack()
        {
            if (AttackIndex == 1)
            {
                AttackIndex = 0;
            }
            else
            {
                AttackIndex++;
            }
            attacking = true;
            _animator.SetBool("isAttacking",attacking);
            _animator.SetInteger("AttackIndex",AttackIndex);
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
