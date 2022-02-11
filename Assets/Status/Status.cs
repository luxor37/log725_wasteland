using UnityEditor;
using UnityEngine;

namespace Status
{
    // A status can be a buff, debuff, element application, damage (over time or one time)
    public class Status : ScriptableObject
    {
        float duration;
        float timer = 0f;
        float initialDmg;
        float perSecDmg;

        string name;
        string animationState;
        string soundToPlay;
        string particleToSpawn;

        public StatusController _controller;

        public void StatusTick(float deltaTime)
        {
            timer += deltaTime;
            if (timer > duration)
            {
                EndStatus();
            }
            
        }

        void EndStatus()
        {
            Destroy(this);
        }
    }
}