using static FXManager;

namespace Status
{

    public class WindStatus : IStatus
    {

        public float duration = 3f;
        public int initialDmg = 0;
        public int perSecDmg = 10;


        public WindStatus(StatusController controller) : base(controller)
        {
            name = "Wind";
            particleToSpawn = ParticleType.Wind;
        }

        public override void StatusTick(float deltaTime)
        {
            if (timer == 0f)
            {
                _controller.TakeDamage(initialDmg);
                _controller.SetParticleSystem(particleToSpawn, duration);
                _controller.KnockUp(0.2f);
            }
            else if (timer - lastTick > 1f && perSecDmg > 0f)
            {
                _controller.TakeDamage(perSecDmg);
                lastTick = timer;
                _controller.FloatDown(0.1f);

            }
        


            timer += deltaTime;
            if (timer > duration)
            {
                EndStatus();
            }

        }
    }
}