using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    Success,
    Failure,
    InProgress
}
public abstract class BTNode
{
   public NodeResult UpdateNode()
   {
        //one off thing
        if(!started)
        {
            started = true;
            //ask the result of the node
            NodeResult executeResult = Execute();
            //if it is not in Progress we finish it
            if(executeResult != NodeResult.InProgress)
            {
                EndNode();
                return executeResult;
            }
        }

        //time based
        NodeResult updateResult = Update();
        if(updateResult != NodeResult.InProgress)
        {
            EndNode();
        }
        return updateResult;
   }

    //==============================================================================================================================================================


    protected virtual NodeResult Update()
    {
        //time based
        return NodeResult.Success;
    }

    //override in child class
    protected virtual NodeResult Execute()
    {
        //one off thing
        return NodeResult.Success;
    }

    private void EndNode()
    {
        started = false;
        End();
    }

    protected virtual void End()
    {
        //clean up on Child Class
    }

    public void Abort()
    {
        EndNode();
    }

    bool started = false;
    int priority;

    public int GetPriority()
    {
        return priority;
    }

    public virtual void SortPriority(ref int priorityConter)
    {
        priority = priorityConter++;
        Debug.Log($"{this} has priority {priority}");
    }
}
