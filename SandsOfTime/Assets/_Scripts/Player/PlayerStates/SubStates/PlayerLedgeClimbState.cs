using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;


    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private PlayerData playerData;

    private bool isHanging;
    private bool isClimbing;

    private int xInput;
    private int yInput;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (Movement.FacingDirection * PlayerData.startOffset.x), cornerPos.y - PlayerData.startOffset.y);
        stopPos.Set(cornerPos.x + (Movement.FacingDirection * PlayerData.stopOffset.x), cornerPos.y + PlayerData.stopOffset.y);

        player.transform.position = startPos;

    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;

            Movement?.SetVelocityZero();
            player.transform.position = startPos;

            if (xInput == Movement.FacingDirection && isHanging && !isClimbing)
            {
                isClimbing = true;
                player.Anim.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollisionSenses.WallCheck.position, Vector2.right * Movement.FacingDirection, CollisionSenses.WallCheckDistance, CollisionSenses.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set(xDist * Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down, CollisionSenses.LedgeCheckHorizontal.position.y - CollisionSenses.WallCheck.position.y, CollisionSenses.WhatIsGround);
        float yDist = yHit.distance;

        workspace.Set(CollisionSenses.WallCheck.position.x + (xDist * Movement.FacingDirection), CollisionSenses.LedgeCheckHorizontal.position.y - yDist);
        return workspace;
    }
}
