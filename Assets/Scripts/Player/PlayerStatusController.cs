using System;
using Status;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class PlayerStatusController : StatusController
    {
        public float timeInvicible;
        private float Timer;
        
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
            if (isInvincible)
            {
                Timer += Time.deltaTime;
                if (Timer < timeInvicible)
                {
                    float remainder = Timer % 0.3f; 
                    transform.GetChild(0).gameObject.SetActive(remainder > 0.15f);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    isInvincible = false;
                    Timer = 0;
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

            var itemcontroller = item.GetComponent<ItemController>();
            if (itemcontroller)
            {
                var status = StatusManager.Instance.GetNewStatusObject(itemcontroller.statusName, this);
                AddStatus(status);
            }
        }

        public void AttackMultiplier(int multiplier, int flat)
        {

            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage *= multiplier;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage *= multiplier;
            ShowStat("x" + multiplier, new Color(0, 1, 1, 1));

            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage += flat;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage += flat;
            ShowStat("+" + flat, new Color(0, 1, 1, 1), 0.5f);

        }

        public void AttackMultiplierRevert(int multiplier, int flat)
        {
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage -= flat;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage -= flat;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().meleeDamage /= multiplier;
            SwitchCharacter.currentCharacter.GetComponent<Player.PlayerAttack>().rangedDamage /= multiplier;
        }

        public void AddCoin()
        {
            PersistenceManager.coins += 1;
        }
        
        public new void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Knockback();
        }

        void PlayerDeath()
        {
            SceneTransitionManager.sceneTransitionManager.GameOver();
        }


    }
}
