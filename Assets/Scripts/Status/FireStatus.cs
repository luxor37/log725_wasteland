using UnityEditor;
using UnityEngine;

namespace Status
{
    
    public class FireStatus : IStatus
    {

        public float duration = 5f;
        public int maxStacks = 1;
        public int curStacks = 0;
        public int initialDmg = 0;
        public int perSecDmg = 50;
        public string particleToSpawn = "FX_Fire_01";


        public FireStatus(StatusController controller) : base(controller)
        {
            name = "Fire";
        }

        private void Start()
        {
            Debug.Log("Fire applied");
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f)
            {
                _controller.TakeDamage(initialDmg);
                _controller.SetParticleSystem(particleToSpawn, duration);
            } else if (timer - lastTick > 1f && perSecDmg > 0f)
            {
                Debug.Log("tick dmg");
                _controller.TakeDamage(perSecDmg);
                lastTick = timer;
            }

           

            timer += deltaTime;
            if (timer > duration)
            {
                EndStatus();
            }
            
        }
    }
}