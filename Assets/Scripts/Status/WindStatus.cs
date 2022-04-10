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
            if (Timer == 0f)
            {
                Controller.TakeDamage(initialDmg);
                Controller.SetParticleSystem(particleToSpawn, duration);
                Controller.KnockUp(0.2f);
            }
            else if (Timer - LastTick > 1f && perSecDmg > 0f)
            {
                Controller.TakeDamage(perSecDmg);
                LastTick = Timer;
                Controller.FloatDown(0.1f);

            }
        


            Timer += deltaTime;
            if (Timer > duration)
            {
                EndStatus();
            }

        }
    }
}