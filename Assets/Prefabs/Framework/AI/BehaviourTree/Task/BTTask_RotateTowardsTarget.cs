using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RotateTowardsTarget : BTNode
{
    BehaviourTree tree;
    string targetKey;
    float acceptableDegrees;
    GameObject targetObject;
    IBehaviourTreeInterface behaviourTreeInterface;
   
    public BTTask_RotateTowardsTarget(BehaviourTree tree, string keyTarget, float acceptableDegrees = 10f)
    {
        this.tree = tree;
        this.targetKey = keyTarget;
        this.acceptableDegrees = acceptableDegrees;

        this.behaviourTreeInterface = tree.GetBehaviourTreeInterface();
    }

    //check if nothing is null and method isInAcceptableDegrees
    protected override NodeResult Execute()
    {
        //if tree or blackboard doesnt exist
        if (tree == null || tree.Blackboard == null)
        {
            return NodeResult.Failure;
        }

        //check interface
        if (behaviourTreeInterface == null)
        {
            return NodeResult.Failure;
        }

        //if there is not data with keyTarget or if targetObject(the ref parameter is null
        if (!tree.Blackboard.GetBlackboardData(targetKey, out targetObject))
        {
            return NodeResult.Failure;
        }

        if(IsInAcceptableDegrees())
            return NodeResult.Success;

        //react to behaviourTreeValueChange
        tree.Blackboard.onBlackboardValueChange += BlackboardValueChanged;

        return NodeResult.InProgress;
    }

    //it changes on real time the targetObject
    private void BlackboardValueChanged(string key, object value)
    {
        if(key == targetKey)
        {
            targetObject = (GameObject)value;
        }
    }

    //check if it is in acceptableDistance
    protected override NodeResult Update()
    {
        if(targetObject == null) return NodeResult.Failure;

        if (IsInAcceptableDegrees())
            return NodeResult.Success;

        behaviourTreeInterface.RotateTowards(targetObject);
        return NodeResult.InProgress;
    }

    bool IsInAcceptableDegrees()
    {
        if (targetKey == null) return false;
        //direction of the player = player pos - enemy pos
        Vector3 targetDir = (targetObject.transform.position - tree.transform.position).normalized;
        //direction of the enemy = look forward of the enemy
        Vector3 dir = tree.transform.forward;

        //angle between direction of the target and the enemy
        float degrees = Vector3.Angle(targetDir, dir);

        //check if it is enough
        return degrees <= acceptableDegrees;
    }

    protected override void End()
    {
        //unsuscribed
        tree.Blackboard.onBlackboardValueChange -= BlackboardValueChanged;
        base.End();
    }
}
