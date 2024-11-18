using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTaskGroup_MoveToLastSeenLocation : BTTask_Group
{
    float acceptableDistance;
    public BTTaskGroup_MoveToLastSeenLocation(BehaviourTree tree, float acceptableDistance = 1) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BTNode Root)
    {
        Sequencer CheckLastSeenLocation = new Sequencer();

        #region Tasks
        //move to the last location
        BTTask_MoveToLocation moveToLastSeenLocation = new BTTask_MoveToLocation(tree, "LastSeenLocation", acceptableDistance);
        //wait for 2 seconds
        BTTask_Wait WaitAtLastSeenLocation = new BTTask_Wait(2f);
        //after checking, clear that data
        BTTask_RemoveBlackboardData removeBlackboardData = new BTTask_RemoveBlackboardData(tree, "LastSeenLocation");
        #endregion

        #region AddingChilds
        //add the childs
        CheckLastSeenLocation.AddChild(moveToLastSeenLocation);
        CheckLastSeenLocation.AddChild(WaitAtLastSeenLocation);
        CheckLastSeenLocation.AddChild(removeBlackboardData);
        #endregion

        #region Decorator
        //check with a blackboard decorator when there is no last seen location to do it or not
        //we send this BT, the sequencer to execute, the key, the condition of if exists, if runConditionChange and abort
        BlackboardDecorator checkLastSeenLocationDecorator = new BlackboardDecorator(tree,
            CheckLastSeenLocation,
            "LastSeenLocation",
            BlackboardDecorator.RunCondition.KeyExists,
            BlackboardDecorator.NotifyRule.RunConditionChange,
            BlackboardDecorator.NotifyAbort.none
            );
        #endregion

        Root = checkLastSeenLocationDecorator;
    }
}
