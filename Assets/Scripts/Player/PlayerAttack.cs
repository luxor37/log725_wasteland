
using System;
using Enemy;
using Status;
using UnityEngine;


namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public enum AttackType { MELEE = 0, RANGED = 1 };
        public ItemController.StatusEnum statusEffectMelee;
        public ItemController.StatusEnum statusEffectRanged;
        public Transform attackPoint;
        public Transform rangedAttackStartPosition;
        public Vector3 attackRange = new Vector3(1, 1, 1);
        public LayerMask enemyLayers;
        public int meleeDamage;
        public int rangedDamage;
        public AttackType attackType;
        private bool attacking = false;

        private Animator _animator;
        private PlayerController _playerController;

        public GameObject rangedWeapon;
        private float rangeTimer = 0f;
        public float rangeCooldown = 1f;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {

            rangeTimer += Time.deltaTime;
            attackType = (AttackType)(InputController.AttackType % Enum.GetNames(typeof(AttackType)).Length);
            if (attackType == AttackType.MELEE && rangedWeapon != null)
                rangedWeapon.SetActive(false);
            else if (attackType == AttackType.RANGED && rangedWeapon != null)
                rangedWeapon.SetActive(true);
            _animator.SetBool("isRanged", attackType == AttackType.RANGED);

            if (InputController.IsAttacking)
            {
                if (PlayerMovementController.canJump && attackType == AttackType.MELEE)
                    Attack();

                if (attackType == AttackType.RANGED)
                    RangeAttack();
            }
            else
            {
               // _animator.SetBool("RangedAttack", false);
            }
        }
        private void Attack()
        {
            _animator.SetTrigger("Attack");
            attacking = true;
        }

        private void RangeAttack()
        {
            if (rangeTimer < rangeCooldown)
                return;
            rangeTimer = 0f;
            if (rangedAttackStartPosition != null)
            {
                _animator.SetBool("RangedAttack", true);
                var newProjectile = Projectile.ProjectileManager.Instance.GetProjectile("Bullet");
                var projectileController = newProjectile.GetComponent<ProjectileController>();
                projectileController.direction = transform.forward;
                projectileController.damage = rangedDamage;
                projectileController.appliedStatus = statusEffectRanged;
                Instantiate(newProjectile, rangedAttackStartPosition.position, attackPoint.rotation);
                attacking = true;
            }
        }

        private void Hit()
        {
            Collider[] hitEnemies = Physics.OverlapBox(attackPoint.position, attackRange, Quaternion.identity, enemyLayers);
            foreach (Collider enemy in hitEnemies)
            {
                IDamageble damagebleable = enemy.GetComponent<IDamageble>();
                if (damagebleable != null)
                {
                    damagebleable.TakeDamage(meleeDamage);
                    var enemyStatusController = enemy.GetComponent<StatusController>();
                    // TODO: be able to change this with element attack system
                    var newStatus = StatusManager.Instance.GetNewStatusObject(statusEffectMelee, enemyStatusController);
                    enemyStatusController.AddStatus(newStatus);
                    enemyStatusController.Knockback();
                }
            }
        }

        private void SetAttacking()
        {
            this.attacking = false;
        }

        public bool GetAttacking()
        {
            return this.attacking;
        }

        private void OnDrawGizmos()
        {
            if (attackPoint == null)
                return;
            Gizmos.DrawWireCube(attackPoint.position, attackRange);
        }
    }
}
