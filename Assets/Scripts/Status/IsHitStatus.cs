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
            if (Timer == 0f)
            {

                (Controller).isHit = true;
            }
            else if (Timer - LastTick > 1f)
            {
                LastTick = Timer;
            }
            
            Timer += deltaTime;
            if (Timer > Duration)
            {
                EndStatus();
            }
        }
    }
}