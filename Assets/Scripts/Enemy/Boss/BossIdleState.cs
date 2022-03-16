using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class BossIdleState : IState
{
    private EnemyStatesController controller;
    private Parameter _parameter;

    public BossIdleState(EnemyStatesController enemyStatesController)
    {
        controller = enemyStatesController;
        this._parameter = controller.parameter;
    }

    public void OnEnter()
    {
        if (controller.distancePlayer() >= 5.5)
        {
            _parameter._animator.SetTrigger("Roar");
        }
    }

    public void OnUpdate()
    {
        if (controller.distancePlayer() > _parameter.attackRange)
        {
            controller.TransitionState(EnemyStateType.Chase);
        }
    }

    public void OnExit()
    {

    }

}

public class BossChaseState : IState
{
    private EnemyStatesController controller;
    private Parameter _parameter;

    public BossChaseState(EnemyStatesController enemyStatesController)
    {
        controller = enemyStatesController;
        this._parameter = controller.parameter;
    }

    public void OnEnter()
    {
        _parameter._NavMeshAgent.SetDestination(_parameter._target.position);
    }

    public void OnUpdate()
    {
        if (_parameter._NavMeshAgent.remainingDistance <= _parameter.attackRange)
        {
            controller.TransitionState(EnemyStateType.Attack);
        }
    }

    public void OnExit()
    {
        
    }
}


public class BossAttackState : IState
{
    private EnemyStatesController controller;
    private Parameter _parameter;

    public BossAttackState(EnemyStatesController enemyStatesController)
    {
        controller = enemyStatesController;
        this._parameter = controller.parameter;
    }

    public void OnEnter()
    {
        _parameter._NavMeshAgent.SetDestination(controller.transform.position);
        _parameter._animator.SetTrigger("Attack");
    }

    public void OnUpdate()
    {
        if (controller.distancePlayer() > _parameter.attackRange)
        {
            controller.TransitionState(EnemyStateType.Idle);
        }
        else
        {
            _parameter._animator.SetTrigger("Attack");
        }
    }

    public void OnExit()
    {
        
    }
}

public class BossResetState : IState
{
    private EnemyStatesController controller;
    private Parameter _parameter;

    public BossResetState(EnemyStatesController enemyStatesController)
    {
        controller = enemyStatesController;
        _parameter = controller.parameter;
    }

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}