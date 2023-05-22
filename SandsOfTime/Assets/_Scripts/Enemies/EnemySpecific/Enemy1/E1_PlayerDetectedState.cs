using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Greg.CoreSystem;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;

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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxAgroGrange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (!isDetecteingLedge)
        {
            Movement?.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
