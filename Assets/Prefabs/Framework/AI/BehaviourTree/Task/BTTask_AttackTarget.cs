using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_AttackTarget : BTNode
{
    BehaviourTree tree;
    string targetKey;
    GameObject target;

    //constructor
    public BTTask_AttackTarget(BehaviourTree tree, string targetKey)
    {
        this.tree = tree;
        this.targetKey = targetKey;
    }
    protected override NodeResult Execute()
    {
        //if tree, blackboard or out parameter target filled is null, it doesnt do anything
        if (!tree || tree.Blackboard == null || !tree.Blackboard.GetBlackboardData(targetKey, out target))
        {
            return NodeResult.Failure;
        }

        //get a reference of the interface
        IBehaviourTreeInterface behaviourTreeInterface = tree.GetBehaviourTreeInterface();
        if (behaviourTreeInterface == null)
        {
            return NodeResult.Failure;
        }

        //we make the functionality
        behaviourTreeInterface.AttackTarget(target);
        return NodeResult.Success;
    }
}
