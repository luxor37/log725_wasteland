using System.Collections;
using UnityEngine;

namespace Status
{
    public class CoinStatus : IStatus
    {

        public float duration = 1f;

        private bool _statusApplied = false;


        public CoinStatus(StatusController controller) : base(controller)
        {
            name = "CoinStatus";
        }

        private void Start()
        {
            Debug.Log("Coin Collected");
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