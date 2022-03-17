﻿using static FXManager;

namespace Status
{
    
    public class FireStatus : IStatus
    {

        public float duration = 5f;
        public int initialDmg = 0;
        public int perSecDmg = 50;
        public ParticleType particleToSpawn = ParticleType.Fire;


        public FireStatus(StatusController controller) : base(controller)
        {
            name = "Fire";
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