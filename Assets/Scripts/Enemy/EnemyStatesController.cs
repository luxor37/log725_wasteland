﻿using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public enum StateType
    {
        Idle, Chase, Attack, Reset
    }

    [Serializable]
    public class Parameter
    {
        public Transform attackPoints;
        public float attackRange;
        
        public int chaseRange = 1;
        public float chaseTime;

        public Vector3 originPosition;
        
        [HideInInspector]
        public Animator _animator;

        public LayerMask layer;

        [HideInInspector]
        public NavMeshAgent _NavMeshAgent;

        [HideInInspector]
        public Transform _target;
    }

    public class EnemyStatesController : MonoBehaviour
    {
        public Parameter parameter;
        private IState currentState;
        private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

        public GameObject attackPoint;

        private void Start()
        {
            parameter._animator = GetComponent<Animator>();
            parameter._NavMeshAgent = GetComponent<NavMeshAgent>();
            parameter.originPosition = this.gameObject.transform.position;
            parameter._NavMeshAgent.stoppingDistance = parameter.attackRange;

            states.Add(StateType.Idle, new IdleState(this));
            states.Add(StateType.Chase, new ChaseState(this));
            states.Add(StateType.Attack, new AttackState(this));
            states.Add(StateType.Reset, new ResetState(this));
            TransitionState(StateType.Idle);
        }

        private void Update()
        {
            currentState.OnUpdate();
            findPlayer();
        }

        public void TransitionState(StateType type)
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = states[type];
            currentState.OnEnter();
        }

        public void FlipTo(Vector3 targetPosition)
        {
            if (targetPosition != null)
            {
                if (transform.position.x > targetPosition.x)
                {
                    transform.localScale = new Vector3(-1,1,1);
                }
                else if (transform.position.x < targetPosition.x)
                {
                    transform.localScale = new Vector3(1,1,1);
                }
            }
        }

        public bool findPlayer()
        {
            Vector3 chaseBox = new Vector3(parameter.chaseRange/2, 1, 1);
            Collider[] players =  Physics.OverlapBox(parameter.attackPoints.position, chaseBox,Quaternion.identity,parameter.layer);
            if (players.Length != 0)
            {
                this.parameter._target = players[0].transform;
                parameter._NavMeshAgent.SetDestination(this.parameter._target.position);
                return true;
            }
            else
            {
                this.parameter._target = null;
                return false;
            }
        }

        public void ZombieAttack()
        {
            Collider[] hitEnemies = Physics.OverlapBox(attackPoint.transform.position, new Vector3(parameter.attackRange,1,1),Quaternion.identity, parameter.layer);
            foreach (Collider player in hitEnemies)
            {
                player.GetComponent<PlayerStatusController>().TakeDamage(100);
            }
        }

        private void OnDrawGizmos()
        {
            if (parameter.attackPoints == null)
                return;
            Vector3 chaseBox = new Vector3(parameter.chaseRange, 1, 1);
            if (parameter._target != null)
            {
                Gizmos.color = Color.red;
                
                Gizmos.DrawWireCube(parameter.attackPoints.position, chaseBox);
            }
            else
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(parameter.attackPoints.position, chaseBox);
            }
        }
    }
}