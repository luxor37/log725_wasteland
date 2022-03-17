
using System;
using Enemy;
using Status;
using UnityEngine;


namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public enum AttackType { MELEE = 0, RANGED = 1 };

        public Transform attackPoint;
        public Vector3 attackRange = new Vector3(1,1,1);
        public LayerMask enemyLayers;
        public int attack;
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
                if(PlayerMovementController.canJump && attackType == AttackType.MELEE)
                    Attack();

                if (attackType == AttackType.RANGED)
                    RangeAttack();
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
            _animator.SetTrigger("Attack");
            var newProjectile = Projectile.ProjectileManager.Instance.GetProjectile("FireProjectile");
            newProjectile.GetComponent<ProjectileController>().direction = transform.forward;
            Instantiate(newProjectile, attackPoint.position, attackPoint.rotation);
            attacking = true;
        }

        private void Hit()
        {
            Collider[] hitEnemies = Physics.OverlapBox(attackPoint.position, attackRange,Quaternion.identity, enemyLayers);
            foreach (Collider enemy in hitEnemies)
            {
                IDamageble damagebleable = enemy.GetComponent<IDamageble>();
                if(damagebleable != null)
                {
                    damagebleable.TakeDamage(attack);
                    var enemyStatusController = enemy.GetComponent<EnemyStatusController>();
                    // TODO: be able to change this with element attack system
                    var newStatus = StatusManager.Instance.GetNewStatusObject(ItemController.StatusEnum.Fire, enemyStatusController);
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
