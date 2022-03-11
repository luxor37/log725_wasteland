
using System;
using System.Collections.Generic;
using System.IO;
using Status;
using Unity.Mathematics;
using UnityEngine;
using Object = System.Object;


namespace Player
{
    public enum AttackName
    {
        MeleeAttack01, MeleeAttack02, MeleeAttack03, RangeAttack01
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
        public bool isRange;

        private Animator _animator;
        private PlayerCharacter _player;
        private PlayerMovementController _playerMovement;
        public TrailRenderer BulletTracer;
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

            if (_playerMovement.IsGrounded)
            {
                RangeMode();
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
                    Physics.OverlapBox(atk.AttackStartPoint.position, new Vector3(atk.DamageRadiusX,1,1), Quaternion.identity, enemyLayers);
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
                Ray ray = default;
                RaycastHit hitInfo;
                
                if (atk.AttackVFX != null)
                {
                    atk.AttackVFX.Emit(1);
                }
                ray.origin = atk.AttackStartPoint.position;
                ray.direction = FindShootTarget(atk);
                var tracer = Instantiate(BulletTracer, ray.origin, Quaternion.identity);
                tracer.AddPosition(ray.origin);
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform != null)
                    {
                        if (atk.HitVFX != null)
                        {
                            atk.HitVFX.transform.position = hitInfo.point;
                            atk.HitVFX.transform.forward = hitInfo.normal;
                            atk.HitVFX.Emit(1);
                        }
                        tracer.transform.position = hitInfo.point;
                    }

                }
            }
        }

        private void meleeAttack(int Stack)
        {
            if (_player.CharacterIndex == 0) //attack action for character 1
            {
                if (Stack == (int)AttackName.MeleeAttack01)
                {
                    Attacks[(int) AttackName.MeleeAttack01].AttackStartPoint = meleAttackPoint;
                    Attacks[(int) AttackName.MeleeAttack01].DamageRadiusX = meleAttackRange.x;
                    Hit(Attacks[(int)AttackName.MeleeAttack01]);
                }
                else if (Stack == (int) AttackName.MeleeAttack02)
                {
                    Attacks[(int) AttackName.MeleeAttack02].AttackStartPoint = meleAttackPoint;
                    Attacks[(int) AttackName.MeleeAttack02].DamageRadiusX = meleAttackRange.x;
                    Hit(Attacks[(int) AttackName.MeleeAttack02]);
                }
                else if (Stack == (int) AttackName.MeleeAttack03)
                {
                    Attacks[(int) AttackName.MeleeAttack03].AttackStartPoint = meleAttackPoint;
                    Attacks[(int) AttackName.MeleeAttack03].DamageRadiusX = meleAttackRange.x;
                    Hit(Attacks[(int) AttackName.MeleeAttack03]);
                }

            }
            else if (_player.CharacterIndex == 1) //attack action for character 2
            {
                
            }
        }

        private void rangeAtack()
        {
            if(_player.CharacterIndex == 0)
            {
                Attacks[(int) AttackName.RangeAttack01].DamageRadiusX = rangeAttackRange;
                Attacks[(int) AttackName.RangeAttack01].AttackStartPoint = rangeAttackPoint;
                Attacks[(int) AttackName.RangeAttack01].AttackVFX = ParticleManager.Instance.GetParticle("ShootFire_edit");
                Attacks[(int) AttackName.RangeAttack01].AttackVFX.transform.position = Attacks[(int) AttackName.RangeAttack01].AttackStartPoint.position;
                Attacks[(int) AttackName.RangeAttack01].HitVFX = ParticleManager.Instance.GetParticle("HitEffect");
                Hit(Attacks[(int)AttackName.RangeAttack01]);
            }
            else if (_player.CharacterIndex == 1) //attack action for character 2
            {
                
            }
        }

        private void RangeMode()
        {
            if (Input.GetButtonDown("WeaponChange"))
            {
                if (isRange == false) isRange = true;
                else isRange = false;
                
                _animator.SetBool("isRanged",isRange);
            }
        }

        private Vector3 FindShootTarget(Attack atk)
        {
            Vector3 center = atk.AttackStartPoint.position;
            float offset = 4 * transform.forward.x;
            center.x =  center.x + offset;
            Collider[] hitEnemies =
                Physics.OverlapBox(center, new Vector3(atk.DamageRadiusX * 2, atk.DamageRadiusY * 2, 1), quaternion.identity, 
                    enemyLayers);
            if (hitEnemies.Length > 0)
            {
                hitEnemies[0].GetComponent<EnemyCharacter>().TakeDamage(atk.BasicAttack);
                return hitEnemies[0].bounds.center - atk.AttackStartPoint.position;
            }

            return new Vector3(transform.forward.x * atk.DamageRadiusX, 0, 0);
        }
        // private void OnDrawGizmos()
        // {
        //     Vector3 center = rangeAttackPoint.position;
        //     float offset = 2 * transform.forward.x;
        //     center.x =  center.x + offset;
        //     Gizmos.DrawWireCube(center, new Vector3(rangeAttackRange, 2, 1));
        // }
    }
}
