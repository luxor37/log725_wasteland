using System.Collections;
using UnityEngine;

namespace Status
{
    public class RecoveryStatus : IStatus
    {
        public float duration = 5f;
        public int maxStacks = 1;
        public int curStacks = 0;
        public int initialHeal = 0;
        public int perSecHeal = 50;
        public string particleToSpawn = "FX_BloodSplat_01";


        public RecoveryStatus(StatusController controller) : base(controller)
        {
            name = "Recovery";
        }

        private void Start()
        {
            Debug.Log("Recovery applied");
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f && initialDmg > 0f)
            {
                _controller.TakeHeal(initialHeal);
            }
            else if (timer - lastTick > 1f && perSecDmg > 0f)
            {
                Debug.Log("tick dmg");
                _controller.TakeHeal(perSecHeal);
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