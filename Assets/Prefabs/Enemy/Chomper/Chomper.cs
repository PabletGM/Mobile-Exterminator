using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy
{
    //override of the interface  IBehaviourTree method
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
    }
}
