using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy2 enemy;

    public E2_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicsUpdate()
    {
        base.LogicsUpdate();

        if(performCloseRangeAction)
        {
            if(Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown) {
                stateMachine.ChangeStatus(enemy.dodgeState);
            } else
            {
                stateMachine.ChangeStatus(enemy.meleeAttackState);
            }
        } 
        else if(performLongRangeAction)
        {
            stateMachine.ChangeStatus(enemy.rangedAttackState);
        }
        else if(!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeStatus(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
