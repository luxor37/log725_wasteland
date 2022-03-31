using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static FXManager;

namespace Status
{
    // Status to be replaced with ScriptableObject
    public abstract class StatusController : GameEntity
    {
        protected Animator _animator;
        protected ParticlesController _particlesController;
        protected NavMeshAgent _agent;
        protected Rigidbody _body;
        public bool isHit = false;
        public bool isShielded = false;
        public List<IStatus> statuses = new List<IStatus>();

        

        private void Start()
        {
            _body = GetComponent<Rigidbody>();    
        }

        protected void Update()
        {
            foreach (IStatus status in statuses.ToArray())
            {
                status.StatusTick(Time.deltaTime);
            }
            List<IStatus> ReactionStatuses = ReactionManager.Instance.GetReactions(statuses, this);
            foreach (IStatus stat in ReactionStatuses)
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
            if (_agent != null)
                _agent.enabled = true;
            var status = statuses.Find(s => s.name == name);
            statuses.Remove(status);
            if (_particlesController != null)
                _particlesController.RemoveParticle(status.particleToSpawn);
        }

        public void Knockback()
        {
            if (!isHit)
            {
                isHit = true;
                if (_animator)
                    _animator.SetTrigger("isHit");
            }
        }

        public void Hit()
        {
            //this.isInvincible = true;
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

        void DisableAI()
        {
            if (_agent != null)
                _agent.enabled = false;
        }

        public void KnockUp(float force)
        {
            if (_body)
            {
                DisableAI();
                _body.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            }
        }

        public void FloatDown(float force)
        {
            if (_body)
            {
                DisableAI();
                _body.AddForce(new Vector3(0, -force, 0), ForceMode.Impulse);
            }
        }

        public void Spin(float force)
        {
            if (_body)
            {
                DisableAI();
                _body.AddTorque(new Vector3(force, force, -force));
            }
            
        }

    }
}