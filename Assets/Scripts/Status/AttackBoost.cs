using Player;
using static FXManager;

namespace Status
{
    public class AttackBoost : IStatus
    {

        public float duration = 5f;
        public int flatBoost = 50;
        public int multiBoost = 2;
        public ParticleType particleToSpawn = ParticleType.AttackBoost;

        public AttackBoost(PlayerStatusController controller) : base(controller)
        {
            name = "AttackBoost";
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (Timer == 0f)
            {
                ((PlayerStatusController)Controller).AttackMultiplier(multiBoost, flatBoost);
                Controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (Timer - LastTick > 1f)
            {
                LastTick = Timer;
            }
            
            Timer += deltaTime;
            if (!(Timer > duration)) return;
            ((PlayerStatusController)Controller).AttackMultiplierRevert(multiBoost, flatBoost);
            EndStatus();

        }
    }
}