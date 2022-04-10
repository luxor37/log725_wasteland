using Player;

namespace Status
{
    public class CoinStatus : IStatus
    {

        public CoinStatus(PlayerStatusController controller) : base(controller)
        {
            name = "CoinStatus";
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (Timer != 0f) return;
            ((PlayerStatusController)Controller).AddCoin();
            EndStatus();

        }
    }
}