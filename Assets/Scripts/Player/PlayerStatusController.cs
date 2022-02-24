using UnityEngine;

namespace Player
{
    public class PlayerStatusController : GameEntity
    {
        private Animator _animator;
        public GameObject floatingPoint;

        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            _animator = this.gameObject.GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            if (floatingPoint != null)
            {
                Instantiate(floatingPoint, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
                floatingPoint.GetComponentInChildren<TextMesh>().text = "-" + damage;
            }
            base.TakeDamage(damage);
        }

        public void TakeHeal(int heal)
        {

            base.TakeHeal(heal);
        }

        public int CalculateDamage()
        {
            return 0;
        }
    }
}
