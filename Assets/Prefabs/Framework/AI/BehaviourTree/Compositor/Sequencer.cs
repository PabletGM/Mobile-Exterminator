using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : Compositor
{
    protected override NodeResult Update()
    {
        //Ask the result of the current child and updateNode method
        NodeResult result = GetCurrentChild().UpdateNode();

        //ifit fails, everything is failed
        if(result == NodeResult.Failure)
        {
            return NodeResult.Failure;
        }
        //if it is corrent, lets go to the next node
        if (result == NodeResult.Success)
        {
            if(Next())
                return NodeResult.InProgress;
            else 
                return NodeResult.Success;
        }
        return NodeResult.InProgress;
    }
}
