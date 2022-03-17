using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class ProjectileManager : MonoBehaviour
    {
        public List<GameObject> projectiles;

        static ProjectileManager instance;

        public static ProjectileManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            } else
            {
                instance = this;
            }
        }

        public GameObject GetProjectile(string name)
        {
            foreach (GameObject projectile in projectiles)
            {
                if (projectile.name.Equals(name))
                    return projectile;
            }
            return null;
        }

        public GameObject getIndex(int i)
        {
            return projectiles[i];
        }
    }
}