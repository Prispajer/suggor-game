using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayerState : State
{

    D_LookPlayerState stateData;

    protected bool turnNow;
    protected bool isPlayerMinRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;
    public LookPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerMinRange = entity.CheckPlayerMinRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (turnNow)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnNow = false;
        }
        else if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }
        if(amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurn(bool flip)
    {
        turnNow = flip;
    }
}
