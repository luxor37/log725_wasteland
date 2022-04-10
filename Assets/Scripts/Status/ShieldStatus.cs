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
            if (Timer == 0f)
            {
                Controller.isInvincible = true;
                Controller.isShielded = true;
                Controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (Timer - LastTick > 1f)
            {
                
                LastTick = Timer;
            }

            Timer += deltaTime;
            if (!(Timer > duration)) return;
            EndStatus();
            Controller.isInvincible = false;
            Controller.isShielded = false;

        }
    }
}