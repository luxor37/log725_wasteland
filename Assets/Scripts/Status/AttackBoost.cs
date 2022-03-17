using static FXManager;

namespace Status
{
    public class AttackBoost : IStatus
    {

        public new float duration = 5f;
        public int flatBoost = 50;
        public int multiBoost = 2;
        public new ParticleType particleToSpawn = ParticleType.AttackBoost;


        public AttackBoost(StatusController controller) : base(controller)
        {
            name = "AttackBoost";
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f)
            {
                _controller.AttackMultiplier(multiBoost, flatBoost);
                _controller.SetParticleSystem(particleToSpawn, duration);
            }
            else if (timer - lastTick > 1f)
            {
                lastTick = timer;
            }

            

            timer += deltaTime;
            if (timer > duration)
            {
                _controller.AttackMultiplierRevert(multiBoost, flatBoost);
                EndStatus();
            }

        }
    }
}