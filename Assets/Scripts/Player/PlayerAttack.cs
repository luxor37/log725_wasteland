using Player;
using Status;
using UnityEngine;
using static ItemController;
using static PersistenceManager;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        private StatusEnum StatusEffectMelee, StatusEffectRanged;
        [SerializeField]
        private Transform AttackPoint, RangedAttackStartPosition;
        [SerializeField]
        private Vector3 AttackRange = new Vector3(1, 1, 1);
        [SerializeField]
        private LayerMask EnemyLayers;
        [SerializeField]
        public int MeleeDamage, RangedDamage;
        [SerializeField]
        private AttackTypeEnum AttackType;
        [SerializeField]
        private GameObject RangedWeapon;
        [SerializeField]
        private float RangeCooldown = 1f;
        [SerializeField]
        private string rangeProjectile = "";
        [SerializeField]
        private float rangeBuildup = 0f;
        [SerializeField]
        private ParticleSystem meleeEffect;

        private float rangeTimer = 0;
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
            
            AttackType = ActiveCharacter == ActiveCharacterEnum.Character2 ? 
                AttackTypeEnum.Ranged : 
                InputController.AttackType;
            
            switch (AttackType)
            {
                case AttackTypeEnum.Melee when PlayerMovementController.CanJump:
                    if (RangedWeapon != null)
                        RangedWeapon.SetActive(false);
                    _animator.SetBool("isRanged", false);
                    if (!InputController.IsAttacking) return;
                    Attack();
                    break;
                case AttackTypeEnum.Ranged when RangedWeapon != null:
                    RangedWeapon.SetActive(true);
                    _animator.SetBool("isRanged", true);
                    if (!InputController.IsAttacking) return;
                    RangeAttack();
                    break;
                case AttackTypeEnum.Ranged when RangedWeapon == null:
                    //RangedWeapon.SetActive(false);
                    _animator.SetBool("isRanged", false);
                    if (!InputController.IsAttacking) return;
                    Attack();
                    RangeAttack();
                    break;
            }

        }
        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private async Task RangeAttackAsync()
        {
            _animator.SetBool("RangedAttack", true);
            await Task.Delay((int)(rangeBuildup * 1000));
            if (RangedAttackStartPosition == null || !_animator.GetBool("RangedAttack")) return;

           
            var newProjectile = Projectile.ProjectileManager.Instance.GetProjectile(rangeProjectile);
            if (newProjectile == null)
                return;
            var projectileController = newProjectile.GetComponent<ProjectileController>();
            projectileController.direction = transform.forward;
            projectileController.damage = RangedDamage;
            projectileController.appliedStatus = StatusEffectRanged;
            Instantiate(newProjectile, RangedAttackStartPosition.position, newProjectile.transform.rotation);
        }

        private async void RangeAttack()
        {
            if (rangeTimer < RangeCooldown) return;
            rangeTimer = 0f;
            await RangeAttackAsync();
           
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
                if (meleeEffect != null)
                    Instantiate(meleeEffect, new Vector3(damageable.transform.position.x, AttackPoint.position.y, AttackPoint.position.z), Quaternion.identity);
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
