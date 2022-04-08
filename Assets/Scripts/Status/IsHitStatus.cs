using Player;

namespace Status
{
    public class IsHitStatus : IStatus
    {
        public float Duration = 1f;

        public IsHitStatus(StatusController controller) : base(controller)
        {
            name = "IsHitStatus";
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f)
            {

                (_controller).isHit = true;
            }
            else if (timer - lastTick > 1f)
            {
                lastTick = timer;
            }
            
            timer += deltaTime;
            if (timer > Duration)
            {
                EndStatus();
            }
        }
    }
}