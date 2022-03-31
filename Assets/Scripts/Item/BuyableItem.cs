using System.Collections;
using UnityEngine;

namespace Item
{
    public class BuyableItem : MonoBehaviour, IBuyable
    {

        // Start is called before the first frame update
        private Collider _bounds;
        private bool _areTouching = false;

        public TextMesh instructions;
        private ItemController item;

        public int cost = 5;

        void Start()
        {
            item = GetComponent<ItemController>();
            if (instructions != null)
            {
                instructions.characterSize = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_areTouching && InputController.VerticalDirection == VerticalDirection.Up)
            {
                if (PersistenceManager.coins >= cost && item)
                {
                    PersistenceManager.coins -= cost;
                  
                    InventoryManager.Instance.getInventory().AddItem(item);
                    Destroy(gameObject);
                }
            }

        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Character")
            {
                _areTouching = true;
                if (instructions != null)
                {
                    instructions.characterSize = 1;
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Character")
            {
                _areTouching = false;
                if (instructions != null)
                {
                    instructions.characterSize = 0;
                }
            }
        }
    }
}