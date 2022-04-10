using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static FXManager;

namespace Status
{
    // Status to be replaced with ScriptableObject
    public abstract class StatusController : GameEntity
    {
        protected Animator Animator;
        protected ParticlesController ParticlesController;
        protected NavMeshAgent Agent;
        protected Rigidbody Body;
        public bool isHit = false;
        public bool isShielded = false;
        public List<IStatus> statuses = new List<IStatus>();

        

        private void Start()
        {
            Body = GetComponent<Rigidbody>();    
        }

        protected void Update()
        {
            foreach (var status in statuses.ToArray())
            {
                status.StatusTick(Time.deltaTime);
            }
            var reactionStatuses = ReactionManager.Instance.GetReactions(statuses, this);
            foreach (var stat in reactionStatuses)
                AddStatus(stat);
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
            foreach (var s in statuses.Where(s => s.name.Equals(status.name)))
            {
                s.AddStack(1);
                return;
            }
            statuses.Add(status);
           
        }

        public void EndStatus(string statusName)
        {
            if (Agent != null)
                Agent.enabled = true;
            var status = statuses.Find(s => s.name == statusName);
            statuses.Remove(status);
            if (ParticlesController != null)
                ParticlesController.RemoveParticle(status.particleToSpawn);
        }

        public void Knockback()
        {
            if (isHit) return;
            isHit = true;
            if (Animator)
                Animator.SetTrigger("isHit");
            AddStatus(StatusManager.Instance.GetNewStatusObject(ItemController.StatusEnum.IsHit, this));
        }

        public void Hit()
        {
            //this.isInvincible = true;
        }
        
        public void ResetHit()
        {
            isHit = false;
        }

        public void SetParticleSystem(ParticleType name, float duration, bool onBody=true)
        {
            if (ParticlesController)
                ParticlesController.ChangeParticles(name, duration, onBody);
        }

        void DisableAI()
        {
            if (Agent != null)
                Agent.enabled = false;
        }

        public void KnockUp(float force)
        {
            if (!Body) return;
            DisableAI();
            Body.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
        }

        public void FloatDown(float force)
        {
            if (!Body) return;
            DisableAI();
            Body.AddForce(new Vector3(0, -force, 0), ForceMode.Impulse);
        }

        public void Spin(float force)
        {
            if (!Body) return;
            DisableAI();
            Body.AddTorque(new Vector3(force, force, 0));

        }

    }
}