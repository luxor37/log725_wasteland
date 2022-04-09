using System;
using Player;
using Status;
using UnityEngine;
using static ItemController;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public enum AttackTypeEnum { Melee = 0, Ranged = 1 };
        public StatusEnum StatusEffectMelee, StatusEffectRanged;
        public Transform AttackPoint, RangedAttackStartPosition;
        public Vector3 AttackRange = new Vector3(1, 1, 1);
        public LayerMask EnemyLayers;
        public int MeleeDamage, RangedDamage;
        public AttackTypeEnum AttackType;
        public GameObject RangedWeapon;
        public float RangeCooldown = 1f;
        
        private float rangeTimer;
        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            rangeTimer += Time.deltaTime;

            if (PersistenceManager.ActiveCharacter == PersistenceManager.ActiveCharacterEnum.character2)
                AttackType = AttackTypeEnum.Melee;
            else
                AttackType = (AttackTypeEnum)(InputController.AttackType % Enum.GetNames(typeof(AttackTypeEnum)).Length);

            switch (AttackType)
            {
                case AttackTypeEnum.Melee when RangedWeapon != null:
                    RangedWeapon.SetActive(false);
                    break;
                case AttackTypeEnum.Ranged when RangedWeapon != null:
                    RangedWeapon.SetActive(true);
                    break;
            }

            _animator.SetBool("isRanged", AttackType == AttackTypeEnum.Ranged);

            if (!InputController.IsAttacking) return;

            if (PlayerMovementController.CanJump && AttackType == AttackTypeEnum.Melee) Attack();

            if (AttackType == AttackTypeEnum.Ranged) RangeAttack();
        }
        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private void RangeAttack()
        {
            if (rangeTimer < RangeCooldown) return;

            rangeTimer = 0f;

            if (RangedAttackStartPosition == null) return;

            _animator.SetBool("RangedAttack", true);
            var newProjectile = Projectile.ProjectileManager.Instance.GetProjectile("Bullet");
            var projectileController = newProjectile.GetComponent<ProjectileController>();
            projectileController.direction = transform.forward;
            projectileController.damage = RangedDamage;
            projectileController.appliedStatus = StatusEffectRanged;
            Instantiate(newProjectile, RangedAttackStartPosition.position, AttackPoint.rotation);
        }

        private void Hit()
        {
            if (gameObject.GetComponent<PlayerStatusController>().isHit) return;


            var hitEnemies =
                Physics.OverlapBox(AttackPoint.position, AttackRange, Quaternion.identity, EnemyLayers);
            foreach (var enemy in hitEnemies)
            {
                var damageable = enemy.GetComponent<GameEntity>();
                if (damageable == null) continue;
                damageable.TakeDamage(MeleeDamage);
                var enemyStatusController = enemy.GetComponent<StatusController>();
                var newStatus = StatusManager.Instance.GetNewStatusObject(StatusEffectMelee, enemyStatusController);
                enemyStatusController.AddStatus(newStatus);
                enemyStatusController.Knockback();
            }
        }

        private void OnDrawGizmos()
        {
            if (AttackPoint == null)
                return;
            Gizmos.DrawWireCube(AttackPoint.position, AttackRange);
        }

        public void SetAttacking()
        {

        }
    }
}
