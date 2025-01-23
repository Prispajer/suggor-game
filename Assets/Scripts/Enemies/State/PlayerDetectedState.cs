using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{

    protected D_PlayerDetectedState stateData;

    protected bool isPlayerMinRange;
    protected bool isPlayerMaxRange;
    protected bool performLongAction;
    protected bool performCloseAction;
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {

        this.stateData = stateData;

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerMinRange = entity.CheckPlayerMinRange();
        isPlayerMaxRange = entity.CheckPlayerMaxRange();

        performCloseAction = entity.CheckPlayerCloseRange();
    }

    public override void Enter()
    {
        base.Enter();

        performLongAction = false;
        entity.SetVelocity(0f);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.longAction)
        {
            performLongAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
