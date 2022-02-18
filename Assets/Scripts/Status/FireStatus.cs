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
            
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f && initialDmg > 0f)
            {
                _controller.TakeDamage(initialDmg);
            } else if (timer - lastTick > 1f && perSecDmg > 0f)
            {
                Debug.Log("tick dmg");
                _controller.TakeDamage(perSecDmg);
                lastTick = timer;
            }

            _controller.SetParticleSystem(particleToSpawn, duration);

            timer += deltaTime;
            if (timer > duration)
            {
                EndStatus();
            }
            
        }
    }
}