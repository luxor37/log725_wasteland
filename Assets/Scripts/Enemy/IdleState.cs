using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

namespace Enemy
{
    public class IdleState : IState
    {
        private EnemyStatesController _enemyStatesController;
        private Parameter _parameter;

        private float timer;
        private float waitTime = 5;
        private bool foundPlayer;

        public IdleState(EnemyStatesController controller)
        {
            this._enemyStatesController = controller;
            this._parameter = controller.parameter;
        }

        public void OnEnter()
        {
            _parameter._animator.SetBool("isWalking", false);
        }

        public void OnUpdate()
        {
            timer += Time.deltaTime;
            if (_parameter._target != null)
            {
                _enemyStatesController.TransitionState(EnemyStateType.Chase);
            }
            if (timer > waitTime)
            {
                _enemyStatesController.TransitionState(EnemyStateType.Seek);
            }

        }

        public void OnExit()
        {
            timer = 0;
        }
    }
    
    public class SeekState : IState
    {
        private EnemyStatesController _enemyStatesController;
        private Parameter _parameter;
        private Vector3 rightEdge;
        private Vector3 leftEdge;
        private Vector3 currentDestination;

        private bool walking;
        private float timer;
        private bool foundEdge;

        public SeekState(EnemyStatesController controller)
        {
            this._enemyStatesController = controller;
            this._parameter = controller.parameter;
        }

        public void OnEnter()
        {
            rightEdge = _enemyStatesController.rightEdge.position;
            leftEdge = _enemyStatesController.leftEdge.position;
            currentDestination = leftEdge;
            _parameter._NavMeshAgent.SetDestination(currentDestination);
            _parameter._NavMeshAgent.stoppingDistance = 0.2f;

        }

        public void OnUpdate()
        {
            if (_parameter._target != null)
            {
                _enemyStatesController.TransitionState(EnemyStateType.Chase);
            }
            if (currentDestination == leftEdge && _parameter._NavMeshAgent.remainingDistance < 0.2)
            {
                _parameter._NavMeshAgent.velocity = Vector3.zero;
                currentDestination = rightEdge;
                _parameter._NavMeshAgent.SetDestination(currentDestination);
            }
            else if (currentDestination == rightEdge && _parameter._NavMeshAgent.remainingDistance < 0.2)
            {
                _parameter._NavMeshAgent.velocity = Vector3.zero;
                currentDestination = leftEdge;
                _parameter._NavMeshAgent.SetDestination(currentDestination);
            }


        }

        public void OnExit()
        {
            timer = 0;
        }
        
    }
    
    public class ChaseState : IState
    {
        private EnemyStatesController _enemyStatesController;
        private Parameter _parameter;
        private Transform _CurrentTarget;
        private float timer;
        
        
        public ChaseState(EnemyStatesController controller)
        {
            this._enemyStatesController = controller;
            this._parameter = controller.parameter;
        }

        public void OnEnter()
        {
            _parameter._animator.SetBool("isWalking", true);
            if(_parameter._target == null)
                _enemyStatesController.TransitionState(EnemyStateType.Reset);
                
            _parameter._NavMeshAgent.SetDestination(_parameter._target.position);
            
        }

        public void OnUpdate()
        {
           
            
            if (_enemyStatesController.findPlayer())
            {
                _CurrentTarget = _parameter._target;
                _parameter._NavMeshAgent.SetDestination(this._parameter._target.transform.position);
                _parameter._NavMeshAgent.stoppingDistance = _parameter.attackRange;
                float remainingDistance = _parameter._NavMeshAgent.remainingDistance;

                if (remainingDistance < _parameter.attackRange)
                {
                    _parameter._animator.SetBool("isWalking", false);
                    this._enemyStatesController.TransitionState(EnemyStateType.Attack);
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > this._parameter.chaseTime)
                {
                    _CurrentTarget = null;
                    this._enemyStatesController.TransitionState(EnemyStateType.Reset);
                }
                if(_CurrentTarget != null)
                    _parameter._NavMeshAgent.SetDestination(_CurrentTarget.position);
                    
            }
        }

        public void OnExit()
        {
            timer = 0;
        }
    }
    
    public class AttackState : IState
    {
        private EnemyStatesController _enemyStatesController;
        private Parameter _parameter;
        private float timer = 0;

        public AttackState(EnemyStatesController controller)
        {
            this._enemyStatesController = controller;
            this._parameter = controller.parameter;
        }

        public void OnEnter()
        {
            _parameter._animator.SetBool("isWalking", false);
            if (_parameter._NavMeshAgent.remainingDistance <= _parameter.attackRange)
            {
                _parameter._NavMeshAgent.SetDestination(_enemyStatesController.transform.position);
            }
        }

        public void OnUpdate()
        {
            if (_enemyStatesController.findPlayer())
            {
                if (_parameter.attackRange > _parameter._NavMeshAgent.remainingDistance)
                {
                    _parameter._NavMeshAgent.SetDestination(_enemyStatesController.transform.position);
                    _parameter._animator.SetTrigger("Attack");
                    _parameter._animator.SetBool("isWalking", false);
                }
            }
            else
            {
                _enemyStatesController.TransitionState(EnemyStateType.Chase);
            }

           
            
        }

        public void OnExit()
        {
            
        }
    }
    
    public class ResetState : IState
    {
        private EnemyStatesController _enemyStatesController;
        private Parameter _parameter;

        public ResetState(EnemyStatesController controller)
        {
            this._enemyStatesController = controller;
            this._parameter = controller.parameter;
        }

        public void OnEnter()
        {
            _parameter._animator.SetBool("isWalking", true);
            _parameter._NavMeshAgent.stoppingDistance = 0.6f;
            _parameter._NavMeshAgent.SetDestination(_parameter.originPosition);
        }

        public void OnUpdate()
        {
            
            
            if (_enemyStatesController.findPlayer())
            {
                _enemyStatesController.TransitionState(EnemyStateType.Chase);
            }
            if (_parameter._NavMeshAgent.remainingDistance < 0.5)
            {
                _parameter._animator.SetBool("isWalking", false);
                _enemyStatesController.TransitionState(EnemyStateType.Idle);
            }
        }

        public void OnExit()
        {
            
        }
    }
}