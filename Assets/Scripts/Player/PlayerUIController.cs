using UnityEngine;
using UnityEngine.UI;


namespace Player
{
    public class PlayerUIController : MonoBehaviour
    {
        public Slider HPStrip;
        private PlayerCharacter _playerCharacter;

        // Start is called before the first frame update
        void Start()
        {
            _playerCharacter = this.gameObject.GetComponent<PlayerCharacter>();
            HPStrip.maxValue = _playerCharacter.maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            HPStrip.value = _playerCharacter.CurrentHealth;;
        }

        void getHP()
        {

        }
    }
}
