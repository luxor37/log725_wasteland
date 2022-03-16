using System;
using System.Collections.Generic;
using Player;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

namespace Enemy
{
    public enum EnemyStateType
    {
        Idle,Seek,Chase, Attack, Reset
    }

    [Serializable]
    public class Parameter
    {
        public Transform attackPoints;
        public float attackRange;
        public int chaseRange = 1;
        public float chaseTime;
        public float movementSpeed;

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
        private Dictionary<EnemyStateType, IState> states = new Dictionary<EnemyStateType, IState>();
        private EnemyCharacter _enemyCharacter;
        public Transform leftEdge;
        public Transform rightEdge;

        private void Start()
        {
            parameter._animator = GetComponent<Animator>();
            parameter._NavMeshAgent = GetComponent<NavMeshAgent>();
            parameter.originPosition = gameObject.transform.position;
            parameter._NavMeshAgent.speed = parameter.movementSpeed;
            parameter._NavMeshAgent.stoppingDistance = parameter.attackRange;
            _enemyCharacter = GetComponent<EnemyCharacter>();
            findPlayer();

            if (_enemyCharacter.EnemyID == (int) EnemyType.Zombie)
            {
                states.Add(EnemyStateType.Idle, new IdleState(this));
                states.Add(EnemyStateType.Seek, new SeekState(this));
                states.Add(EnemyStateType.Chase, new ChaseState(this));
                states.Add(EnemyStateType.Attack, new AttackState(this));
                states.Add(EnemyStateType.Reset, new ResetState(this));
                TransitionState(EnemyStateType.Idle);
            }
            else if (_enemyCharacter.EnemyID == (int) EnemyType.BossOne)
            {
                states.Add(EnemyStateType.Idle, new BossIdleState(this));
                states.Add(EnemyStateType.Chase, new BossChaseState(this));
                states.Add(EnemyStateType.Attack, new BossAttackState(this));
                states.Add(EnemyStateType.Reset, new BossResetState(this));
                TransitionState(EnemyStateType.Idle);
            }


        }

        private void Update()
        {
            findPlayer();
            if(parameter._target != null) LookAtTarget(parameter._target);
            flipHorizontal();
            parameter._animator.SetBool("isWalking", parameter._NavMeshAgent.velocity.magnitude > 0.1f);
            currentState.OnUpdate();
            
        }

        public void TransitionState(EnemyStateType type)
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

        public void flipHorizontal()
        {
            if (parameter._NavMeshAgent.velocity.x < 0)
            {
                transform.forward = Vector3.left;
            }
            if (parameter._NavMeshAgent.velocity.x > 0)
            {
                transform.forward = Vector3.right;
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
        
        public float distancePlayer()
        {
            return Vector3.Distance(transform.position, parameter._target.position);
        }
        
        public void disableMovement()
        {
            parameter._NavMeshAgent.speed = 0;
        }

        public void resetMovement()
        {
            parameter._NavMeshAgent.speed = parameter.movementSpeed;
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