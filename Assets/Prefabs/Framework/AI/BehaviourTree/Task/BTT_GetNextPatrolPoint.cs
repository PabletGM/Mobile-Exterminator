using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTT_GetNextPatrolPoint : BTNode
{
    PatrollingComponent patrollingComp;
    BehaviourTree tree;
    string patrolPointKey;
   public BTT_GetNextPatrolPoint(BehaviourTree tree, string patrolPointKey)
    {
        //get patrolling component
        patrollingComp = tree.GetComponent<PatrollingComponent>();
        //BT
        this.tree = tree;
        //patrol point key
        this.patrolPointKey = patrolPointKey;
    }

    protected override NodeResult Execute()
    {
        //if it exist component and there is nextPatrolPoint
        if(patrollingComp != null && patrollingComp.GetNextPatrolPoint(out Vector3 point))
        {
            tree.Blackboard.SetOrAddData(patrolPointKey, point);
            return NodeResult.Success;
        }
        //IF NOT
        return NodeResult.Failure;
    }
}
