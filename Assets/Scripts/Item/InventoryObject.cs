using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Item
{
    [CreateAssetMenu(fileName = "InventoryObject", menuName = "ScriptableObjects/Inventory/InventoryObject", order = 1)]
    public class InventoryObject : ScriptableObject
    {
        
        Dictionary<ItemController, int> inventoryObjects;
        public int maxInventorySize = 4;


        private void Awake()
        {
            inventoryObjects = new Dictionary<ItemController, int>();
        }

        public void AddItem(ItemController item)
        {
            if (inventoryObjects == null)
                inventoryObjects = new Dictionary<ItemController,int>();
            if (inventoryObjects.ContainsKey(item))
                inventoryObjects[item]++;
            else if (inventoryObjects.Count < maxInventorySize)
                inventoryObjects.Add(item, 1);
            Debug.Log(inventoryObjects[item]);
        }

        public void UseItem(ItemController item, Player.PlayerStatusController playerStatusController)
        {
            if (inventoryObjects.ContainsKey(item))
            {
                inventoryObjects[item]--;
                item.ApplyItem(playerStatusController);

                if (inventoryObjects[item] == 0)
                    inventoryObjects.Remove(item);
            }
                
        }
    }
}