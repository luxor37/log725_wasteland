using Player;
using UnityEditor;

namespace Enemy
{
    public class EnemyStatusController : GameEntity
    {
        protected override void Start()
        {
            base.Start();
            
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public int getCurrentHealth()
        {
            return this.currentHealth;
        }
    }
}