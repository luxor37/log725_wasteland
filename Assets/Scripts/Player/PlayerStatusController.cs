using UnityEngine;

namespace Player
{
    public class PlayerStatusController : GameEntity
    {
        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            _animator = this.gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public int CalculateDamage()
        {
            return 0;
        }
    }
}
