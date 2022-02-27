using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Status
{
    // subclass sandbox (I guess component sandbox in this case) pattern. Statuses should only affect the game by using methods available here.
    // every component that might be affected by Status should be referenced here.
    public class StatusController : MonoBehaviour
    {
        private Animator _animator;
        private ParticlesController _particlesController;
        private GameCharacter gameCharacter;
        private bool isHit;

        public List<IStatus> statuses;

        // TODO: reference
        // MovementController
        // AnimationController
        // ParticleController
        // SoundController
        // AIController
        void Start()
        {
            _animator = GetComponent<Animator>();
            _particlesController = GetComponent<ParticlesController>();
            gameCharacter = GetComponent<GameCharacter>();
            statuses = new List<IStatus>();
        }

        // Update is called once per frame
        void Update()
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
                if (s.GetName().Equals(status.GetName()))
                {
                    s.AddStack(1);
                    return;
                }
            }
            statuses.Add(status);
        }

        public void EndStatus(string name)
        {         
            statuses.Remove(statuses.Find(s => s.GetName() == name));
        }

        public void TakeDamage(int damage)
        {
            // adjust damage based on stats, buffs, etc
            // call TakeDamage from component (GameEntity for now)
            
            if (gameObject.tag == "Player")
            {
                gameCharacter.TakeDamage(damage);
            } 
            
        }

        public void TakeHeal(int heal)
        {
            if (gameObject.tag == "Player")
            {
                gameCharacter.TakeHeal(heal);
            } 
        }

        public void AttackMultiplier(int multiplier, int flat)
        {
          
            gameObject.GetComponent<Player.PlayerAttack>().attack *= multiplier;
            gameObject.GetComponent<Player.PlayerAttack>().attack += flat;
            
        }

        public void AttackMultiplierRevert(int multiplier, int flat)
        {
            gameObject.GetComponent<Player.PlayerAttack>().attack -= flat;
            gameObject.GetComponent<Player.PlayerAttack>().attack /= multiplier;
        }
        
        public void ResetHit()
        {
            isHit = false;
        }

        public void SetParticleSystem(string name, float duration)
        {
            if (_particlesController)
                _particlesController.ChangeParticles(name, duration);
        }

    }
}