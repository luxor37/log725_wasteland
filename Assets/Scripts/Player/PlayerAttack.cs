
using System;
using System.Collections.Generic;
using Status;
using UnityEngine;
using Object = System.Object;


namespace Player
{
    public enum AttackName
    {
        MeleeAttack01, MeleeAttack02, MeleeAttack03
    }
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Mele Attack Setting")]
        public Transform meleAttackPoint;
        public Vector3  meleAttackRange = new Vector3(1, 1, 1);
        
        [Header("Range Attack Setting")]
        public Transform rangeAttackPoint;
        public int rangeAttackRange;

        private List<Attack> Attacks;

        private Animator _animator;
        private PlayerCharacter _player;
        private PlayerMovementController _playerMovement;
        public LayerMask enemyLayers;
        
        // Start is called before the first frame update
        void Start()
        {
            _player = GetComponent<PlayerCharacter>();
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovementController>();
            if (_player.CharacterIndex == 0)
            {
                Attacks = AttackManager.Instance.getPlayerAttacks(0);
            }
            else if (_player.CharacterIndex == 1)
            {
                
            }

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
                    Physics.OverlapBox(atk.AttackPoint.position, atk.DamageRadius, Quaternion.identity, enemyLayers);
                foreach (Collider enemies in hitEnemies)
                {
                    if (atk.Debuff != null)
                    {
                       atk.Debuff.AddHandler(enemies.GetComponent<StatusHandler>());
                       enemies.GetComponent<StatusHandler>().AddStatus(atk.Debuff);
                    }
                    enemies.GetComponent<EnemyCharacter>().TakeDamage(atk.BasicAttack);
                }
            }
            else if (atk.AttackType == AttackType.Single)
            {
                //TODO : use Raycast 
                
            }
        }

        private void meleeAttack(int Stack)
        {
            if (_player.CharacterIndex == 0) //attack action for character 1
            {
                if (Stack == (int)AttackName.MeleeAttack01)
                {
                    Attacks[(int) AttackName.MeleeAttack01].AttackPoint = meleAttackPoint;
                    Attacks[(int) AttackName.MeleeAttack01].DamageRadius = meleAttackRange;
                    Hit(Attacks[(int)AttackName.MeleeAttack01]);
                }
                else if (Stack == (int) AttackName.MeleeAttack02)
                {
                    Attacks[(int) AttackName.MeleeAttack02].AttackPoint = meleAttackPoint;
                    Attacks[(int) AttackName.MeleeAttack02].DamageRadius = meleAttackRange;
                    Hit(Attacks[(int) AttackName.MeleeAttack02]);
                }
                else if (Stack == (int) AttackName.MeleeAttack03)
                {
                    Attacks[(int) AttackName.MeleeAttack03].AttackPoint = meleAttackPoint;
                    Attacks[(int) AttackName.MeleeAttack03].DamageRadius = meleAttackRange;
                    Hit(Attacks[(int) AttackName.MeleeAttack03]);
                }

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
