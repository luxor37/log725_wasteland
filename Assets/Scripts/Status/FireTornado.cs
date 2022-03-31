using static FXManager;

namespace Status
{

    public class FireTornado : IStatus
    {

        public float duration = 3f;
        public int initialDmg = 0;
        public int perSecDmg = 150;
        public ParticleType particleToSpawn = ParticleType.FireTornado;

        public FireTornado(StatusController controller) : base(controller)
        {
            name = "FireTornado";
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f)
            {
                _controller.TakeDamage(initialDmg);
                _controller.SetParticleSystem(particleToSpawn, duration);
                // _controller.Spin(100);
                //_controller.KnockUp(0.5f);
            }
            else if (timer - lastTick > 1f && perSecDmg > 0f)
            {
                _controller.TakeDamage(perSecDmg);
                lastTick = timer;
                //_controller.FloatDown(1);
            }



            timer += deltaTime;
            if (timer > duration)
            {
                EndStatus();
            }

        }
    }
}