using UnityEngine;
using UnityEngine.UI;


namespace Player
{
    public class PlayerUIController : MonoBehaviour
    {
        public Slider HPStrip;
        public Slider ShieldCooldown;
        private GameEntity entity;
        private PlayerController controller;
        public Text HealthPotionQuantity;
        public Text AtkBoostQuantity;

        // Start is called before the first frame update
        void Start()
        {
            entity = gameObject.GetComponent<GameEntity>();
            controller = gameObject.GetComponent<PlayerController>();

            HPStrip.maxValue = entity.maxHealth;

            ShieldCooldown.value = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (controller.isShielded)
                ShieldCooldown.value = 0;
            else if(controller.shieldTimer == -1f){
                ShieldCooldown.value = 1;
            }
            else{
                ShieldCooldown.value = controller.shieldTimer/controller.shieldCooldown;
            }
            HPStrip.value = entity.currentHealth;

            HealthPotionQuantity.text = PersistenceManager.HealthPotionAmount + "";
            AtkBoostQuantity.text = PersistenceManager.AtkBoostAmount + "";
        }
    }
}
