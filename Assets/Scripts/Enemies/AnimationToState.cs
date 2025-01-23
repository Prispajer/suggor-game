using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToState : MonoBehaviour
{
    public AttackState attackState;

    private void startAttack()
    {
        attackState.startAttack();
    }
    private void finishAttack()
    {
        attackState.finishAttack();
    }

}
