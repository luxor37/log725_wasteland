using static FXManager;

namespace Status
{

    // A status can be a buff, debuff, element application, damage (over time or one time)
    public abstract class IStatus
    {
        
        public int maxStacks = 1;
        public int curStacks = 0;
        public string name;
        public ParticleType particleToSpawn;


        protected StatusController Controller;

        protected float Timer = 0f;
        protected float LastTick = 0f;

        protected IStatus(StatusController controller)
        {
            Controller = controller;
        }

        public abstract void StatusTick(float time);

        public void EndStatus()
        {
            Controller.EndStatus(name);
        }

        public void AddStack(int stacks)
        {
            if (curStacks + stacks < maxStacks)
                curStacks += stacks;
        }
    }
}
