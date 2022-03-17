using System.Collections.Generic;
using UnityEngine;
using static FXManager;

namespace Status
{
    // Status to be replaced with ScriptableObject
    public abstract class StatusController : GameEntity
    {
        protected Animator _animator;
        protected ParticlesController _particlesController;
        protected bool isHit = false;

        public List<IStatus> statuses = new List<IStatus>();

        protected void Update()
        {
            foreach (IStatus status in statuses.ToArray())
            {
                status.StatusTick(Time.deltaTime);
            }
        }

        public List<IStatus> GetStatuses()
        {
            return statuses;
        }

        public void AddStatus(IStatus status)
        {
            if (status == null)
                return;
            // check duplicate first
            foreach (IStatus s in statuses)
            {
                if (s.name.Equals(status.name))
                {
                    s.AddStack(1);
                    return;
                }
            }
            statuses.Add(status);
           
        }

        public void EndStatus(string name)
        {
            statuses.Remove(statuses.Find(s => s.name == name));
        }

        public void Knockback()
        {
            if (!isHit)
            {
                isHit = true;
                _animator.SetTrigger("isHit");
            }
        }

        public void ResetHit()
        {
            isHit = false;
        }

        public void SetParticleSystem(ParticleType name, float duration)
        {
            if (_particlesController)
                _particlesController.ChangeParticles(name, duration);
        }

    }
}