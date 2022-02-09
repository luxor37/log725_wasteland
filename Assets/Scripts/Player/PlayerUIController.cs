using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUIController : MonoBehaviour
    {
        public Slider HPStrip;
        private int currentHP;
        private int maxHP;

        // Start is called before the first frame update
        void Awake()
        {
            HPStrip.value = currentHP;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void getHP()
        {
        
        }
    }
}
