using UnityEngine;
using UnityEngine.UI;


namespace Player
{
    public class PlayerUIController : MonoBehaviour
    {
        public Slider HPStrip;
        private GameEntity entity;

        // Start is called before the first frame update
        void Start()
        {
            entity = gameObject.GetComponent<GameEntity>();

            HPStrip.maxValue = entity.maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            HPStrip.value = entity.currentHealth;;
        }
    }
}
