using static FXManager;

namespace Status
{
    public class RecoveryStatus : IStatus
    {
        public float duration = 5f;
        public int initialHeal = 100;
        public int perSecHeal = 50;
        public ParticleType particleToSpawn = ParticleType.HealthRegen;


        public RecoveryStatus(StatusController controller) : base(controller)
        {
            name = "Recovery";
        }

        public override void StatusTick(float deltaTime)
        {
            if (Timer == 0f)
            {
                Controller.TakeHeal(initialHeal);
                Controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (Timer - LastTick > 1f && perSecHeal > 0f)
            {
                Controller.TakeHeal(perSecHeal);
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