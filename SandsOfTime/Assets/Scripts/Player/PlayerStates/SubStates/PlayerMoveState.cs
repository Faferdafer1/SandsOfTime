using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
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

        player.SetVeloccityX(PlayerData.movementVelocity * input.x);

        if(input.x == 0f)
        {
            stateMachine.ChangeState(player.IdleState); 
        }
    }

    public override void PhsicsUpdate()
    {
        base.PhsicsUpdate();
    }
}