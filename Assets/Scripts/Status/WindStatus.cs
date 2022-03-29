using static FXManager;

namespace Status
{

    public class WindStatus : IStatus
    {

        public float duration = 2f;
        public int initialDmg = 0;
        public int perSecDmg = 10;
        public ParticleType particleToSpawn = ParticleType.Wind;


        public WindStatus(StatusController controller) : base(controller)
        {
            name = "Wind";
        }

        public override void StatusTick(float deltaTime)
        {
            if (timer == 0f)
            {
                _controller.TakeDamage(initialDmg);
                _controller.SetParticleSystem(particleToSpawn, duration);
                _controller.KnockUp(0.5f);
            }
            else if (timer - lastTick > 1f && perSecDmg > 0f)
            {
                _controller.TakeDamage(perSecDmg);
                lastTick = timer;
                _controller.FloatDown(1);

            }
        


            timer += deltaTime;
            if (timer > duration)
            {
                EndStatus();
            }

        }
    }
}