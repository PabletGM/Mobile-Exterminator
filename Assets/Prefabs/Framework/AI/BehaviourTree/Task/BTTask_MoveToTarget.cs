using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToTarget : BTNode
{
   NavMeshAgent agent;
   string targetKey;
   GameObject target;
   float acceptableDistance = 2;
   BehaviourTree tree;

    //constructor
    public BTTask_MoveToTarget(BehaviourTree tree, string targetKey, float acceptableDistance = 2)
    {
        this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;
        this.tree = tree;
    }

    protected override NodeResult Execute()
    {
        //check if it exist the blackboard and the target parameter on blackboard
        Blackboard blackboard = tree.Blackboard;
        if(blackboard == null || !blackboard.GetBlackboardData(targetKey, out target))
        {
            return NodeResult.Failure;
        }

        //if agent exists
        agent = tree.GetComponent<NavMeshAgent>();
        if(agent == null)
        {
            return NodeResult.Failure;
        }

        //are we on an acceptableDistance?
        if(IsTargetInAcceptableDistance())
        {
            return NodeResult.Success;
        }

        //we add the method to the event
        blackboard.onBlackboardValueChange += BlackboardValueChanged;

        //we make the destination for AI
        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        return NodeResult.InProgress;
    }

    private void BlackboardValueChanged(string key, object value)
    {
        if(key == targetKey)
        {
            target = (GameObject)value;
        }
    }

    protected override NodeResult Update()
    {

        //checking the target because it can change position and gameOject
        if(target == null)
        {
            //to stop the agent
            agent.isStopped = true;
            return NodeResult.Failure;
        }

        agent.SetDestination(target.transform.position);

        //if it is on radius
        if (IsTargetInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.InProgress;
    }
    bool IsTargetInAcceptableDistance()
    {
        return Vector3.Distance(target.transform.position, agent.transform.position) <= acceptableDistance;
    }

    protected override void End()
    {
        agent.isStopped = true;
        //when the blackboard value changes, the method Blackboard Value Changed will be called
        tree.Blackboard.onBlackboardValueChange += BlackboardValueChanged;
        base.End();
    }
}
