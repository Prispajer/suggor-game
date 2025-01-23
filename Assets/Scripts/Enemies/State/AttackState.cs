using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinish;
    protected bool isPlayerMinRange;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerMinRange = entity.CheckPlayerMinRange();
    }

    public override void Enter()
    {
        base.Enter();
        entity.ats.attackState = this;
        isAnimationFinish = false;
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void startAttack()
    {

    }
    public virtual void finishAttack()
    {
        isAnimationFinish = true;
    }
}
