using System.Collections;
using UnityEngine;

namespace Status
{
    public class RecoveryStatus : IStatus
    {
        public float duration = 5f;
        public int maxStacks = 1;
        public int curStacks = 0;
        public int initialHeal = 100;
        public int perSecHeal = 50;
        public string particleToSpawn = "FX_BloodSplat_01";


        public RecoveryStatus()
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
            if (timer == 0f)
            {
                Handler.TakeHeal(initialHeal);
                Handler.SetParticleSystem(particleToSpawn, duration);
            }
            else if (timer - lastTick > 1f && perSecHeal > 0f)
            {
                Debug.Log("tick dmg");
                Handler.TakeHeal(perSecHeal);
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