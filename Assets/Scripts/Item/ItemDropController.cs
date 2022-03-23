using UnityEngine;

namespace Item
{
    public class ItemDropController : MonoBehaviour
    {

        public string itemName;

        public bool getRandomItem;

        GameEntity entity;

        private void Start()
        {
            entity = GetComponent<GameEntity>();
            if(entity == null)
                entity.onDeath += DropObject;
        }

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