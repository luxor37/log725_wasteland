using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Projectile
{
    public class ProjectileManager : MonoBehaviour
    {
        public List<GameObject> projectiles;

        public static ProjectileManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            } else
            {
                Instance = this;
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