using System.Collections;
using UnityEngine;

namespace Status
{
    public class AttackBoost : IStatus
    {

        public float duration = 5f;
        public int maxStacks = 1;
        public int curStacks = 0;
        public int initialBoost = 50;
        public int perSecBoost = 0;
        // public string particleToSpawn = "";


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
            if (timer == 0f && initialDmg > 0f)
            {
                _controller.AttackMultiplier(initialBoost);
            }
            else if (timer - lastTick > 1f && perSecDmg > 0f)
            {
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