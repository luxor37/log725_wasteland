using UnityEngine;

namespace Item
{
    public class ItemDropController : MonoBehaviour
    {

        public string itemName;

        public bool getRandomItem;

        public int dropNum = 1;

        GameEntity entity;

        private void Start()
        {
            entity = GetComponent<GameEntity>();
            if(entity != null)
            {
                for (int i = 0; i < dropNum; i++)
                    entity.onDeath += DropObject;
            }
                
        }

        public void DropObject()
        {
            var itemToSpawn = getRandomItem? ItemManager.Instance.GetRandomItem() : ItemManager.Instance.GetItem(itemName);
            if (itemToSpawn != null)
            {
                Instantiate(itemToSpawn, transform.position + new Vector3(UnityEngine.Random.Range(-1, 1), 0, 0), transform.rotation);
            }
               
        }
    }
}