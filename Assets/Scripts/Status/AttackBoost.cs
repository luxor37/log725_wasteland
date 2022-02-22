using System.Collections;
using UnityEngine;

namespace Status
{
    public class AttackBoost : IStatus
    {

        public float duration = 5f;
        public int maxStacks = 1;
        public int curStacks = 0;
        public int flatBoost = 50;
        public int multiBoost = 2;
        public string particleToSpawn = "Fx_Sparks_01";


        public AttackBoost(StatusController controller) : base(controller)
        {
            name = "AttackBoost";
        }

        private void Start()
        {
            Debug.Log("Attack boost applied");
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f)
            {
                _controller.AttackMultiplier(multiBoost, flatBoost);
                _controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (timer - lastTick > 1f)
            {
                lastTick = timer;
            }

            

            timer += deltaTime;
            if (timer > duration)
            {
                _controller.AttackMultiplierRevert(multiBoost, flatBoost);
                EndStatus();
            }

        }
    }
}