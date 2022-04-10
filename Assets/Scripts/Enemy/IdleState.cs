using UnityEngine;

namespace Enemy
{
    public class IdleState : IState
    {
        private readonly EnemyStatesController _enemyStatesController;
        private readonly Parameter _parameter;

        private float timer;

        public IdleState(EnemyStatesController controller)
        {
            _enemyStatesController = controller;
            _parameter = controller.Parameter;
        }

        public void OnEnter()
        {
            _parameter.Animator.SetBool("isWalking", false);
        }

        public void OnUpdate()
        {

            if (_parameter.Target != null)
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
        private readonly EnemyStatesController _enemyStatesController;
        private readonly Parameter _parameter;

        private float timer;

        public ChaseState(EnemyStatesController controller)
        {
            _enemyStatesController = controller;
            _parameter = controller.Parameter;
        }

        public void OnEnter()
        {
            _parameter.Animator.SetBool("isWalking", true);
        }

        public void OnUpdate()
        {

            timer += Time.deltaTime;
            if (_enemyStatesController.FindPlayer())
            {
                _enemyStatesController.FlipTo(_parameter.Target.position);
                _parameter.NavMeshAgent.SetDestination(this._parameter.Target.transform.position);
                _parameter.NavMeshAgent.stoppingDistance = _parameter.AttackRange;
                var remainingDistance = _parameter.NavMeshAgent.remainingDistance;

                if (!(remainingDistance < _parameter.AttackRange)) return;

                _parameter.Animator.SetBool("isWalking", false);
                _enemyStatesController.TransitionState(StateType.Attack);
            }
            else
            {
                if (timer > _parameter.ChaseTime)
                {
                    _enemyStatesController.TransitionState(StateType.Reset);
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
        private readonly EnemyStatesController _enemyStatesController;
        private readonly Parameter _parameter;

        public AttackState(EnemyStatesController controller)
        {
            _enemyStatesController = controller;
            _parameter = controller.Parameter;
        }

        public void OnEnter()
        {
            _parameter.Animator.SetBool("isWalking", false);
        }

        public void OnUpdate()
        {
            if (_parameter.Target != null)
            {
                _enemyStatesController.FlipTo(_parameter.Target.position);
            }
            else
            {
                Debug.Log("Target is null. Is is dead?");
            }
            
            if (_enemyStatesController.FindPlayer())
            {
                if (_parameter.AttackRange > _parameter.NavMeshAgent.remainingDistance)
                {
                    _parameter.Animator.SetTrigger("Attack");
                    _parameter.Animator.SetBool("isWalking", true);
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
        private readonly EnemyStatesController _enemyStatesController;
        private readonly Parameter _parameter;

        public ResetState(EnemyStatesController controller)
        {
            _enemyStatesController = controller;
            _parameter = controller.Parameter;
        }

        public void OnEnter()
        {
            _parameter.Animator.SetBool("isWalking", true);
            _parameter.NavMeshAgent.stoppingDistance = 0.5f;
            if (_parameter.NavMeshAgent.enabled)
                _parameter.NavMeshAgent.SetDestination(_parameter.OriginPosition);
        }

        public void OnUpdate()
        {
            _enemyStatesController.FlipTo(_parameter.OriginPosition);

            if (_enemyStatesController.FindPlayer())
            {
                _enemyStatesController.TransitionState(StateType.Chase);
            }

            if (!_parameter.NavMeshAgent.enabled || !(_parameter.NavMeshAgent.remainingDistance < 0.5)) return;

            _parameter.Animator.SetBool("isWalking", false);
            _enemyStatesController.TransitionState(StateType.Idle);
        }

        public void OnExit()
        {

        }
    }
}