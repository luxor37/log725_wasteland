using System.Collections;
using UnityEngine;

namespace Status
{
    // subclass sandbox (I guess component sandbox in this case) pattern. Statuses should only affect the game by using methods available here.
    // every component that might be affected by Status should be referenced here.
    public class StatusController : MonoBehaviour
    {
        Status[] statuses;

       // MovementController
       // AnimationController
       // ParticleController
       // SoundController
       // AIController

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            foreach (var status in statuses)
            {
                status.StatusTick(Time.deltaTime);
            }
        }

        public Status[] GetStatuses()
        {
            return statuses;
        }
    }
}