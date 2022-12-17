using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
            stateMachine.ChangeStatus(enemy.meleeAttackState);
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeStatus(enemy.chargeState);
        } else if(!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeStatus(enemy.lookForPlayerState);
        } else if(!isDetectingLedge)
        {
            enemy.Flip();
            stateMachine.ChangeStatus(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
