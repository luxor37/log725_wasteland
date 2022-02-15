using UnityEngine;

namespace Player
{
    public class PlayerCharacter : GameCharacter
    {
        private bool canJump = true;

        protected override void Start()
        {
            base.Start();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public override int CalculateDamage()
        {
            return base.CalculateDamage();
        }

        public bool CanJump
        {
            get => canJump;
            set => canJump = value;
        }

        public int CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }
    }
}