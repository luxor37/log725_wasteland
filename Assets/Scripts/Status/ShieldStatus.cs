using static FXManager;

namespace Status
{
    public class ShieldStatus : IStatus
    {
        public float duration = 2f;
        public int initialHeal = 0;
        public int perSecHeal = 50;
        public ParticleType particleToSpawn = ParticleType.Shield;


        public ShieldStatus(StatusController controller) : base(controller)
        {
            name = "Shield";
        }

        public override void StatusTick(float deltaTime)
        {
            if (timer == 0f)
            {
                _controller.isInvincible = true;
                _controller.isShielded = true;
                _controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (timer - lastTick > 1f)
            {
                
                lastTick = timer;
            }

            timer += deltaTime;
            if (timer > duration)
            {
                EndStatus();
                _controller.isInvincible = false;
                _controller.isShielded = false;
            }

        }
    }
}