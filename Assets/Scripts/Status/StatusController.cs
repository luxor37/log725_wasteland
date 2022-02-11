﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Status
{
    // subclass sandbox (I guess component sandbox in this case) pattern. Statuses should only affect the game by using methods available here.
    // every component that might be affected by Status should be referenced here.
    public class StatusController : GameEntity
    {
        private Animator _animator;

        private bool isHit;

        public List<IStatus> statuses;

        // TODO: reference
        // MovementController
        // AnimationController
        // ParticleController
        // SoundController
        // AIController

        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            statuses = new List<IStatus>();
        }

        // Update is called once per frame
        void Update()
        {
            foreach (IStatus status in statuses)
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
            // check duplicate first
            //foreach (IStatus s in statuses)
            //{
           //     if (s.GetName().Equals(status.GetName()))
          //      {
          //          s.AddStack(1);
         //           return;
           //     }
           // }
            statuses.Add(status);
        }

        public void EndStatus(string name)
        {
            //foreach (IStatus s in statuses)
           // {
            //    if (s.GetName().Equals(name))
            //    {
           //         statuses.Remove(s);
            //    }
           // }
            statuses = new List<IStatus>();

        }

        public void TakeDamage(int damage)
        {
            // adjust damage based on stats, buffs, etc
            // call TakeDamage from component (GameEntity for now)
            base.TakeDamage(damage);
        }

        public int getCurrentHealth()
        {
            return this.currentHealth;
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

        public void SetAnimationState(string name, bool state)
        {

        }

    }
}