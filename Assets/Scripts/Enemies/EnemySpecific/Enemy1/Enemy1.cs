using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_MoveState moveState { get; private set; }
    public E1_IdleState idleState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookPlayerState lookPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_StunState StunState { get; private set; }
    public E1_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookPlayerState lookPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_StunState StunStateData;
    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private D_DeadState deadStateData;

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookPlayerState = new E1_LookPlayerState(this, stateMachine, "lookPlayer", lookPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        StunState = new E1_StunState(this, stateMachine, "stun", StunStateData, this);
        DeadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);


        stateMachine.Initialize(moveState);

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (isDead)
        {
            stateMachine.ChangeState(DeadState);
        }
        else if (isStunned && stateMachine.currentState != StunState)
        {
            stateMachine.ChangeState(StunState);
        }
        
    }


}
