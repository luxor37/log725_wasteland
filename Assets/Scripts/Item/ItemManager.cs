using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ItemManager : MonoBehaviour
    {

        public List<GameObject> items;

        static ItemManager instance = null;

        public static ItemManager Instance
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

        public GameObject GetItem(string itemname)
        {
            foreach (var item in items)
            {
                if (item.name == itemname)
                {
                    return item;
                }
            }
            return null;
        }
    }
}