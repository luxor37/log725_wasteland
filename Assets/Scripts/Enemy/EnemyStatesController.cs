using System;
using System.Collections.Generic;
using Player;
using Status;
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
        public Transform AttackPoint;
        public float AttackRange = 1.25f;
        public int Damage = 50;
        
        public int ChaseRange = 12;
        public float ChaseTime = 5;

        public Vector3 OriginPosition;
        
        [HideInInspector]
        public Animator Animator;

        public LayerMask Layer;

        [HideInInspector]
        public NavMeshAgent NavMeshAgent;

        [HideInInspector]
        public Transform Target;
    }

    public class EnemyStatesController : MonoBehaviour
    {
        private float currentChaseRange;
        public Parameter Parameter;
        private IState currentState;
        private readonly Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
        private StatusController _enemyStatus;

        private void Start()
        {
            Parameter.Animator = GetComponent<Animator>();
            Parameter.NavMeshAgent = GetComponent<NavMeshAgent>();
            Parameter.OriginPosition = gameObject.transform.position;
            Parameter.NavMeshAgent.stoppingDistance = Parameter.AttackRange;
            currentChaseRange = Parameter.ChaseRange;
            _enemyStatus = GetComponent<StatusController>();
            states.Add(StateType.Idle, new IdleState(this));
            states.Add(StateType.Chase, new ChaseState(this));
            states.Add(StateType.Attack, new AttackState(this));
            states.Add(StateType.Reset, new ResetState(this));
            TransitionState(StateType.Idle);
        }

        private void Update()
        {
            currentState.OnUpdate();
            FindPlayer();
        }

        public void TransitionState(StateType type)
        {
            currentState?.OnExit();

            currentState = states[type];
            currentState.OnEnter();
        }

        public void FlipTo(Vector3 targetPosition)
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

        public bool FindPlayer()
        {
            currentChaseRange = Parameter.ChaseRange;
            if (_enemyStatus.isHit)
            {
                currentChaseRange = 20;
            }
            var chaseBox = new Vector3(currentChaseRange/2, 1, 1);
            var players =  Physics.OverlapBox(Parameter.AttackPoint.position, 
                chaseBox,Quaternion.identity,Parameter.Layer);
            if (players.Length != 0 && Parameter.NavMeshAgent.enabled)
            {
                Parameter.Target = players[0].transform;
                Parameter.NavMeshAgent.SetDestination(Parameter.Target.position);

                return true;
            }

            Parameter.Target = null;
            return false;
        }

        public void ZombieAttack()
        {
            if (gameObject.GetComponent<StatusController>().isHit) return;

            var hitEnemies = Physics.OverlapBox(Parameter.AttackPoint.transform.position, 
                new Vector3(Parameter.AttackRange,1,1),Quaternion.identity, Parameter.Layer);
            foreach (var player in hitEnemies)
            {
                player.GetComponent<PlayerStatusController>().TakeDamage(Parameter.Damage);
            }
        }

        private void OnDrawGizmos()
        {
            if (Parameter.AttackPoint == null) return;
            var chaseBox = new Vector3(currentChaseRange, 4, 2);
            if (Parameter.Target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(Parameter.AttackPoint.position, chaseBox);
            }
            else
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(Parameter.AttackPoint.position, chaseBox);
            }
        }
    }
}