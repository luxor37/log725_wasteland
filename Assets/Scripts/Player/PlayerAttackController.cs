
using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;


namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        public Transform attackPoint;
        public Vector3 attackRange = new Vector3(1,1,1);
        public LayerMask enemyLayers;
        public int attack;
        
        private bool attacking = false;
        private int AttackIndex;

        private Animator _animator;
        private PlayerCharacter _playerCharacter;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _playerCharacter = this.gameObject.GetComponent<PlayerCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InputController.IsAttacking)
            {
                //TODO change this
                if(_playerCharacter.CanJump)
                    Attack();
            }
        }
        private void Attack()
        {
            _animator.SetTrigger("Attack");
            attacking = true;
        }

        private void Hit(Attack atk)
        {
            if (atk.AttackType == AttackType.Aoe)
            {
                Collider[] hitEnemies = Physics.OverlapBox(attackPoint.position, attackRange,Quaternion.identity, enemyLayers);
                foreach (Collider enemies in hitEnemies)
                {
                    //TODO : play vfx
                    //TODO : play sound
                    enemies.GetComponent<EnemyCharacter>().TakeDamage(atk);
                }
            }
            else if(atk.AttackType == AttackType.Single)
            {
                //TODO : use Raycast 
            }


        }

        public void MeleeAttack()
        {
            Attack atk = new Attack(_playerCharacter.basicAttack, AttackType.Aoe, AttackForm.Melee, attackPoint, null, null,CharacterElement.Fire);
            Hit(atk);
        }
        
        private void OnDrawGizmos()
        {
            if (attackPoint == null)
                return;
            Gizmos.DrawWireCube(attackPoint.position, attackRange);
        }
    }
}
