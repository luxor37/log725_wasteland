using UnityEngine;

namespace Enemy
{
    public class IdleState : IState
    {
        private EnemyStatesController _enemyStatesController;
        private Parameter _parameter;

        private float timer;

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

            if (_parameter._target != null)
            {
                _enemyStatesController.TransitionState(StateType.Chase);
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

        private float timer;

        public ChaseState(EnemyStatesController controller)
        {
            this._enemyStatesController = controller;
            this._parameter = controller.parameter;
        }

        public void OnEnter()
        {
            _parameter._animator.SetBool("isWalking", true);
        }

        public void OnUpdate()
        {

            timer += Time.deltaTime;
            if (_enemyStatesController.findPlayer())
            {
                _enemyStatesController.FlipTo(_parameter._target.position);
                _parameter._NavMeshAgent.SetDestination(this._parameter._target.transform.position);
                _parameter._NavMeshAgent.stoppingDistance = _parameter.attackRange;
                float remainingDistance = _parameter._NavMeshAgent.remainingDistance;

                if (remainingDistance < _parameter.attackRange)
                {
                    _parameter._animator.SetBool("isWalking", false);
                    this._enemyStatesController.TransitionState(StateType.Attack);
                }
            }
            else
            {
                if (timer > this._parameter.chaseTime)
                {
                    this._enemyStatesController.TransitionState(StateType.Reset);
                }
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

        public AttackState(EnemyStatesController controller)
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
            if (_parameter._target != null)
            {
                _enemyStatesController.FlipTo(_parameter._target.position);
            }
            else
            {
                Debug.Log("Target is null. Is is dead?");
            }
            
            if (_enemyStatesController.findPlayer())
            {
                if (_parameter.attackRange > _parameter._NavMeshAgent.remainingDistance)
                {
                    _parameter._animator.SetTrigger("Attack");
                    _parameter._animator.SetBool("isWalking", true);
                }
                else
                {
                    _enemyStatesController.TransitionState(StateType.Chase);
                }
            }
            else
            {
                _enemyStatesController.TransitionState(StateType.Reset);
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
            _parameter._NavMeshAgent.stoppingDistance = 0.5f;
            _parameter._NavMeshAgent.SetDestination(_parameter.originPosition);
        }

        public void OnUpdate()
        {
            _enemyStatesController.FlipTo(_parameter.originPosition);

            if (_enemyStatesController.findPlayer())
            {
                _enemyStatesController.TransitionState(StateType.Chase);
            }
            if (_parameter._NavMeshAgent.remainingDistance < 0.5)
            {
                _parameter._animator.SetBool("isWalking", false);
                _enemyStatesController.TransitionState(StateType.Idle);
            }
        }

        public void OnExit()
        {

        }
    }
}