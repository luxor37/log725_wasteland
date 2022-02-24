using System.Collections;
using UnityEngine;

namespace Item
{
    public class ItemDropController : MonoBehaviour
    {

        public string itemName;

        public bool getRandomItem;

        private void OnDestroy()
        {
            var itemToSpawn = getRandomItem? ItemManager.Instance.GetRandomItem() : ItemManager.Instance.GetItem(itemName);
            if (itemToSpawn != null)
            {
                Debug.Log(itemName + " Dropped");
                Instantiate(itemToSpawn, transform.position, transform.rotation);
            }
               
        }
    }
}