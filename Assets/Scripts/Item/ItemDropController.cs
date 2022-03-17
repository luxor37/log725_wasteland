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
                Instantiate(itemToSpawn, transform.position, transform.rotation);
            }
               
        }
    }
}