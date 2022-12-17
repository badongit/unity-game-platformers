using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : StunState
{
    private Enemy1 enemy;

    public E1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.ChangeStatus(enemy.meleeAttackState);
            } else if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeStatus(enemy.chargeState);
            } else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeStatus(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
