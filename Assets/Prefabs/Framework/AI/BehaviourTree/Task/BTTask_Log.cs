using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Log : BTNode
{
    string Log;

    //constructor
    public BTTask_Log(string log)
    {
        this.Log = log;
    }

    protected override NodeResult Execute()
    {
        if (Log != "")
        {
            Debug.Log(Log);
            return NodeResult.Success;
        }
        return NodeResult.InProgress;
    }
}
