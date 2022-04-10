using Assets.Scripts.Player;
using Status;
using UnityEngine;
using static SceneTransitionManager;

namespace Player
{
    public class PlayerStatusController : StatusController
    {
        public float TimeInvincible;
        private float _timer;
        
        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _particlesController = GameObject.FindGameObjectWithTag("Player").GetComponent<ParticlesController>();

            base.onDeath += PlayerDeath;
        }

        private void FixedUpdate()
        {
            if (isInvincible && !isShielded)
            {
                _timer += Time.deltaTime;
                if (_timer < TimeInvincible)
                {
                    var remainder = _timer % 0.3f; 
                    transform.GetChild(0).gameObject.SetActive(remainder > 0.15f);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    isInvincible = false;
                    _timer = 0;
                }
            }
        }

        private void Update()
        {
            base.Update();
            GameObject item = null;
            if (InputController.IsUsingItem1 && PersistenceManager.HealthPotionAmount > 0)
            {
                PersistenceManager.HealthPotionAmount -= 1;
                item = Item.ItemManager.Instance.GetItem("RecoverItem1");
            }

            if (InputController.IsUsingItem2 && PersistenceManager.AtkBoostAmount > 0)
            {
                PersistenceManager.AtkBoostAmount -= 1;
                item = Item.ItemManager.Instance.GetItem("AttackBoostItem1");
            }
            if (!item)
                return;

            if (item is null) return;

            var itemController = item.GetComponent<ItemController>();


            if (!itemController) return;

            var status = StatusManager.Instance.GetNewStatusObject(itemController.statusName, this);
            AddStatus(status);
        }

        public void AttackMultiplier(int multiplier, int flat)
        {

            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().MeleeDamage *= multiplier;
            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().RangedDamage *= multiplier;
            ShowStat("x" + multiplier, new Color(0, 1, 1, 1));

            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().MeleeDamage += flat;
            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().RangedDamage += flat;
            ShowStat("+" + flat, new Color(0, 1, 1, 1), 0.5f);

        }

        public void AttackMultiplierRevert(int multiplier, int flat)
        {
            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().MeleeDamage -= flat;
            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().RangedDamage -= flat;
            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().MeleeDamage /= multiplier;
            SwitchCharacter.currentCharacter.GetComponent<PlayerAttack>().RangedDamage /= multiplier;
        }

        public void AddCoin()
        {
            PersistenceManager.coins += 1;
        }
        
        public new void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (!isInvincible)
                Knockback();
        }

        private static void PlayerDeath()
        {
            SceneTransitionManagerSingleton.GameOver();
        }


    }
}
