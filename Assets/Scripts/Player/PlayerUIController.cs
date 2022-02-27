using UnityEngine;
using UnityEngine.UI;


namespace Player
{
    public class PlayerUIController : MonoBehaviour
    {
        public Slider HPStrip;
        private GameCharacter entity;

        // Start is called before the first frame update
        void Start()
        {
            entity = gameObject.GetComponent<GameCharacter>();

            HPStrip.maxValue = entity.maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            HPStrip.value = entity.currentHealth;;
        }

        void getHP()
        {

        }
    }
}
