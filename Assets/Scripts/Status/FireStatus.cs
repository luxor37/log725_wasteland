using static FXManager;

namespace Status
{
    
    public class FireStatus : IStatus
    {

        public float duration = 5f;
        public int initialDmg = 0;
        public int perSecDmg = 50;

        public FireStatus(StatusController controller) : base(controller)
        {
            particleToSpawn = ParticleType.Fire;
            name = "Fire";
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (Timer == 0f)
            {
                Controller.TakeDamage(initialDmg);
                Controller.SetParticleSystem(particleToSpawn, duration);
            } else if (Timer - LastTick > 1f && perSecDmg > 0f)
            {
                Controller.TakeDamage(perSecDmg);
                LastTick = Timer;
            }

            Timer += deltaTime;
            if (Timer > duration)
            {
                EndStatus();
            }
            
        }
    }
}