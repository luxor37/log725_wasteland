using UnityEditor;
using UnityEngine;


namespace Status
{
    // A status can be a buff, debuff, element application, damage (over time or one time)
    public abstract class IStatus
    {

        public float duration;
        public int maxStacks = 1;
        public int curStacks = 0;
        public int initialDmg;
        public int perSecDmg;
        public string soundToPlay;
        public string particleToSpawn;
        public string name;
        
        protected StatusHandler Handler;

        protected float timer = 0f;
        protected float lastTick = 0f;

        protected IStatus()
        {
        }

        public void AddHandler(StatusHandler handler)
        {
            Handler = handler;
        }

        abstract public void StatusTick(float time);
        public void EndStatus()
        {
            Handler.EndStatus(name);
        }

        public string GetName()
        {
            return name;
        }

        public void AddStack(int stacks)
        {
            if (curStacks + stacks < maxStacks)
                curStacks += stacks;
        }
    }
}
