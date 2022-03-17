using static FXManager;

namespace Status
{
    public class RecoveryStatus : IStatus
    {
        public new float duration = 5f;
        public int initialHeal = 100;
        public int perSecHeal = 50;
        public new ParticleType particleToSpawn = ParticleType.HealthRegen;


        public RecoveryStatus(StatusController controller) : base(controller)
        {
            name = "Recovery";
        }

        public override void StatusTick(float deltaTime)
        {
            if (timer == 0f)
            {
                _controller.TakeHeal(initialHeal);
                _controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (timer - lastTick > 1f && perSecHeal > 0f)
            {
                _controller.TakeHeal(perSecHeal);
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