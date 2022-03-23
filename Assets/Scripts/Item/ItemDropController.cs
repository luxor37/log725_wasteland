using UnityEngine;

namespace Item
{
    public class ItemDropController : MonoBehaviour
    {

        public string itemName;

        public bool getRandomItem;

        public void DropObject()
        {
            var itemToSpawn = getRandomItem? ItemManager.Instance.GetRandomItem() : ItemManager.Instance.GetItem(itemName);
            if (itemToSpawn != null)
            {
                Instantiate(itemToSpawn, transform.position, transform.rotation);
            }
               
        }
    }
}