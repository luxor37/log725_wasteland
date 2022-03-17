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
            if (timer == 0f)
            {
                ((PlayerStatusController)_controller).AttackMultiplier(multiBoost, flatBoost);
                _controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (timer - lastTick > 1f)
            {
                lastTick = timer;
            }

            

            timer += deltaTime;
            if (timer > duration)
            {
                ((PlayerStatusController)_controller).AttackMultiplierRevert(multiBoost, flatBoost);
                EndStatus();
            }

        }
    }
}