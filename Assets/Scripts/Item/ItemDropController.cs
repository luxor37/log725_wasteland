using UnityEngine;

namespace Item
{
    public class ItemDropController : MonoBehaviour
    {
        public int DropNum = 1;

        private GameEntity entity;

        private void Start()
        {
            entity = GetComponent<GameEntity>();
            if (entity == null) return;

            for (var i = 0; i < DropNum; i++)
                entity.onDeath += DropObject;

        }

        public void DropObject()
        {
            var itemToSpawn = ItemManager.Instance.GetRandomItem();
            
            if (itemToSpawn == null) return;

            var spawnPosition = transform.position + new Vector3(UnityEngine.Random.Range(-1, 1), 0, 0);
            Instantiate(itemToSpawn, new Vector3(spawnPosition.x, spawnPosition.y, 0), transform.rotation);

        }
    }
}