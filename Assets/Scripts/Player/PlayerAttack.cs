
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Mele Attack Setting")]
        public Transform meleAttackPoint;
        public Vector3  meleAttackRange = new Vector3(1, 1, 1);
        
        [Header("Range Attack Setting")]
        public Transform rangeAttackPoint;
        public int rangeAttackRange;
        
        private Animator _animator;
        private PlayerCharacter _player;
        public LayerMask enemyLayers;
        
        // Start is called before the first frame update
        void Start()
        {
            _player = GetComponent<PlayerCharacter>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InputController.IsAttacking)
            {
                Attack();
            }
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private void Hit(Attack atk)
        {
            if (atk.AttackType == AttackType.Aoe)
            {
                Collider[] hitEnemies =
                    Physics.OverlapBox(meleAttackPoint.position, meleAttackRange, Quaternion.identity, enemyLayers);
                foreach (Collider enemies in hitEnemies)
                {
                    enemies.GetComponent<EnemyCharacter>().TakeDamage(atk.BasicAttack);
                }
            }
            else if (atk.AttackType == AttackType.Single)
            {
                //TODO : use Raycast 
                
            }


        }

        private void meleeAttack()
        {
            if (_player.CharacterIndex == 0) //attack action for character 1
            {
                Attack Punch_one = new Attack(_player.basicAttack, AttackType.Aoe, AttackForm.Melee, meleAttackPoint, null, null,CharacterElement.Fire);
                Hit(Punch_one);
                
            }
            else if (_player.CharacterIndex == 1) //attack action for character 2
            {
                
            }
        }

        // private void OnDrawGizmos()
        // {
        //     if (meleAttackPoint == null)
        //         return;
        //     Gizmos.DrawWireCube(meleAttackPoint.position, meleAttackRange);
        // }
    }
}
