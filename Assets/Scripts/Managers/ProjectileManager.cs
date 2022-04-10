using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Projectile
{
    public class ProjectileManager : MonoBehaviour
    {
        public List<GameObject> projectiles;

        static ProjectileManager instance;

        public static ProjectileManager Instance => instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            } else
            {
                instance = this;
            }
        }

        public GameObject GetProjectile(string name)
        {
            return projectiles.FirstOrDefault(projectile => projectile.name.Equals(name));
        }

        public GameObject getIndex(int i)
        {
            return projectiles[i];
        }
    }
}