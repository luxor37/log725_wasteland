using System.Collections;
using UnityEngine;

namespace Status
{
    public class CoinStatus : IStatus
    {
        public CoinStatus(StatusController controller) : base(controller)
        {
            name = "CoinStatus";
        }

        public override void StatusTick(float deltaTime)
        {
            // damage
            if (timer == 0f)
            {
                
                _controller.AddCoin();
                EndStatus();
            }
            
        }
    }
}