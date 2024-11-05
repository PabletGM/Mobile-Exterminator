using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BTTask_MoveToLocation : BTNode
{
    NavMeshAgent agent;
    string patrolPointKey;
    Vector3 loc;
    float acceptableDistance = 1;
    BehaviourTree tree;

    //constructor
    public BTTask_MoveToLocation(BehaviourTree tree, string patrolPointKey, float acceptableDistance = 1)
    {
        this.patrolPointKey = patrolPointKey;
        this.acceptableDistance = acceptableDistance;
        this.tree = tree;
    }

    protected override NodeResult Execute()
    {
        //check if it exist the blackboard and the target parameter on blackboard
        Blackboard blackboard = tree.Blackboard;
        if (blackboard == null || !blackboard.GetBlackboardData(patrolPointKey, out loc))
        {
            return NodeResult.Failure;
        }

        //if agent exists
        agent = tree.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            return NodeResult.Failure;
        }

        //are we on an acceptableDistance?
        if (IsLocationInAcceptableDistance())
        {
            return NodeResult.Success;
        }

       

        //we make the destination for AI
        agent.SetDestination(loc);
        agent.isStopped = false;
        return NodeResult.InProgress;
    }

    protected override NodeResult Update()
    {
       if(IsLocationInAcceptableDistance())
       {
            agent.isStopped = true;
            return NodeResult.Success;
       }
        return NodeResult.InProgress;
    }

    bool IsLocationInAcceptableDistance()
    {
        return Vector3.Distance(loc, tree.transform.position) <= acceptableDistance;
    }
}
