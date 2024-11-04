using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Compositor
{
    protected override NodeResult Update()
    {
        //Ask the result of the current child and updateNode method
        NodeResult result = GetCurrentChild().UpdateNode();

        //if it is correct we leave
        if (result == NodeResult.Success)
        {
            return NodeResult.Success;
        }

        //ifit fails, everything is failed
        if (result == NodeResult.Failure)
        {
            //can go to the next?
            if(Next())
                return NodeResult.InProgress;
            else
                return NodeResult.Failure;
        }

        return NodeResult.InProgress;
    }
}
