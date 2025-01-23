using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseAction;
    protected bool isPlayerMinRange;

    protected D_StunState stateData;
    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entity.CheckGround();
        performCloseAction = entity.CheckPlayerCloseRange();
        isPlayerMinRange = entity.CheckPlayerMinRange();
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
        isMovementStopped = false;
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbuckAngle, entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStun();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entity.SetVelocity(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
