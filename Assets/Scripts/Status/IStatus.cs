using static FXManager;

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
        public string animationState;
        public string soundToPlay;
        public ParticleType particleToSpawn;
        public string name;
        
        protected StatusController _controller;

        protected float timer = 0f;
        protected float lastTick = 0f;

        protected IStatus(StatusController controller)
        {
            _controller = controller;
        }

        abstract public void StatusTick(float time);

        public void EndStatus()
        {
            _controller.EndStatus(name);
        }

        public void AddStack(int stacks)
        {
            if (curStacks + stacks < maxStacks)
                curStacks += stacks;
        }
    }
}
