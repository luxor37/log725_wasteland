using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class InventoryManager : MonoBehaviour
    {

       // public List<GameObject> items;

        static InventoryManager instance = null;

        [SerializeField]
        private InventoryObject inventoryObject;

        public static InventoryManager Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        public InventoryObject getInventory()
        {
            return inventoryObject;
        }
    }
}