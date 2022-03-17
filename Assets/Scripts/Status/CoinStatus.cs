using System.Collections;
using Player;
using UnityEngine;

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
            if (timer == 0f)
            {
                
                ((PlayerStatusController)_controller).AddCoin();
                EndStatus();
            }
            
        }
    }
}