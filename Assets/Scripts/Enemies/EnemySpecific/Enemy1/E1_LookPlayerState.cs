public class E1_LookPlayerState : LookPlayerState
{
    private Enemy1 enemy;
    public E1_LookPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookPlayerState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerMinRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }

        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
