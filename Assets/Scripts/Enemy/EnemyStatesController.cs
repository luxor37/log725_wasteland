using System;
using System.Collections.Generic;
using Player;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

namespace Enemy
{
    public enum StateType
    {
        Idle,Seek,Chase, Attack, Reset
    }

    [Serializable]
    public class Parameter
    {
        public Transform attackPoints;
        public Transform edgeCheckPoint;
        public float attackRange;
        public int chaseRange = 1;
        public float chaseTime;

        public Vector3 originPosition;
        public Animator _animator;
        public LayerMask layer;
        public NavMeshAgent _NavMeshAgent;
        public Transform _target;
        
    }

    public class EnemyStatesController : MonoBehaviour
    {
        public Parameter parameter;
        private IState currentState;
        private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
        private EnemyCharacter _enemyCharacter;
        public Transform leftEdge;
        public Transform rightEdge;

        private void Start()
        {
            parameter._animator = GetComponent<Animator>();
            parameter._NavMeshAgent = GetComponent<NavMeshAgent>();
            parameter.originPosition = gameObject.transform.position;
            parameter._NavMeshAgent.stoppingDistance = parameter.attackRange;
            parameter._NavMeshAgent.updatePosition = false;
            _enemyCharacter = GetComponent<EnemyCharacter>();
            

            states.Add(StateType.Idle, new IdleState(this));
            states.Add(StateType.Seek, new SeekState(this));
            states.Add(StateType.Chase, new ChaseState(this));
            states.Add(StateType.Attack, new AttackState(this));
            states.Add(StateType.Reset, new ResetState(this));
            TransitionState(StateType.Idle);
        }

        private void Update()
        {
            findPlayer();
            if(parameter._target != null) LookAtTarget(parameter._target);
            currentState.OnUpdate();
            
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

        public void LookAtTarget(Transform targetPosition)
        {
            float direction = targetPosition.position.x - transform.position.x;
            if (direction > 0)
            {
                transform.forward = Vector3.right;
            }
            else if (direction <= 0)
            {
                transform.forward = Vector3.left;
            }
        }

        public bool findPlayer()
        {
            Vector3 chaseBox = new Vector3(parameter.chaseRange/2, 1, 1);
            Collider[] players =  Physics.OverlapBox(parameter.attackPoints.position, chaseBox,Quaternion.identity,parameter.layer);
            if (players.Length != 0)
            {
                this.parameter._target = players[0].transform;
                return true;
            }
            this.parameter._target = null;
            return false;
        }

      

        private void OnAnimatorMove()
        {
            Vector3 position =  parameter._animator.rootPosition;
            parameter._NavMeshAgent.nextPosition = new Vector3(position.x, parameter._NavMeshAgent.nextPosition.y, position.z);
            position.y = parameter._NavMeshAgent.nextPosition.y;
            transform.position = position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                leftEdge = other.transform.GetChild(0);
                rightEdge = other.transform.GetChild(1);
            }
        }
        

        // private void OnDrawGizmos()
        // {
        //     if (parameter.attackPoints == null)
        //         return;
        //     Vector3 chaseBox = new Vector3(parameter.chaseRange, 1, 1);
        //     if (parameter._target != null)
        //     {
        //         Gizmos.color = Color.red;
        //         
        //         Gizmos.DrawWireCube(parameter.attackPoints.position, chaseBox);
        //     }
        //     else
        //     {
        //         Gizmos.color = Color.white;
        //         Gizmos.DrawWireCube(parameter.attackPoints.position, chaseBox);
        //     }
        //     
        // }

       
    }
}