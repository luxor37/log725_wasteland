using System;
using UnityEngine;

namespace Player
{
    public class PlayerCharacter : GameCharacter
    {
        private Animator _animator;
        private int characterIndex = 0;
        public GameObject floatingPoint;
       

        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            if (gameObject)
            {
                if (gameObject.name == "Character1")
                {
                    characterIndex = 0;
                }
                else
                {
                    characterIndex = 1;
                }
            }
            _animator = this.gameObject.GetComponent<Animator>();
        }

        public void TakeDamage(Attack atk)
        {
            showFloatingDamage(atk.BasicAttack);
            base.TakeDamage(atk.BasicAttack);
        }

        public void TakeHeal(int heal)
        {

            base.TakeHeal(heal);
        }

        public int CalculateDamage()
        {
            return 0;
        }
        
        private void showFloatingDamage(int damage)
        {
            if (floatingPoint != null)
            {
                Instantiate(floatingPoint, transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
                floatingPoint.GetComponentInChildren<TextMesh>().text = "-" + damage;
            }
        }

        public int CharacterIndex
        {
            get => characterIndex;
            set => characterIndex = value;
        }
    }
}
