using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RemoveBlackboardData : BTNode
{
      BehaviourTree tree;
      string KeyToRemove; 
      
      //constructor
      public BTTask_RemoveBlackboardData(BehaviourTree tree, string keyToRemove)
      {
            this.tree = tree;
            this.KeyToRemove = keyToRemove;
      }

    protected override NodeResult Execute()
    {
        if(tree != null)
        {
            tree.Blackboard.RemoveBlackboardData(this.KeyToRemove);
            return NodeResult.Success;
        }  
        
        return NodeResult.Failure;
    }
}
