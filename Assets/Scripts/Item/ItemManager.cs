using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Item
{
    public class ItemManager : MonoBehaviour
    {

        public List<GameObject> Items;

        public static ItemManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public GameObject GetItem(string itemName)
        {
            return Items.FirstOrDefault(item => item.name == itemName);
        }

        public GameObject GetRandomItem()
        {
            var randIndex = Random.Range(0, Items.Count);
            
            return Items[randIndex];
        }
    }
}