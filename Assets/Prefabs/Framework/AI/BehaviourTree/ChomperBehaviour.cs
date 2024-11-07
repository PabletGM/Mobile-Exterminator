using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        //create a selector
        Selector rootSelector = new Selector();

        #region Attack

            //create an attack sequencer
            Sequencer attackTargetSeq = new Sequencer();

            #region AddTaskMoveToTargetToAttackSequence

            //create task moveToTarget
            BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(this, "Target");
            //rotate towards the target

            //attack

            //add it to the sequencer
            attackTargetSeq.AddChild(moveToTarget);

            #endregion

            #region Adding DecoratorToSelector

            //THE DECORATOR INCLUDES the child attackTargetSequencer and his task moveToTarget

            //create decorator with some conditions, if Target exists
            BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
                attackTargetSeq, "Target",
                BlackboardDecorator.RunCondition.KeyExists,
                BlackboardDecorator.NotifyRule.RunConditionChange,
                BlackboardDecorator.NotifyAbort.both);

            //add decorator to the selector
            rootSelector.AddChild(attackTargetDecorator);

        #endregion

        #endregion

        #region CheckLastSeenLocationSequencer

        Sequencer CheckLastSeenLocation = new Sequencer();

            #region Tasks
                //move to the last location
                BTTask_MoveToLocation moveToLastSeenLocation = new BTTask_MoveToLocation(this,"LastSeenLocation",2);
                //wait for 2 seconds
                BTTask_Wait WaitAtLastSeenLocation = new BTTask_Wait(2f);
                //after checking, clear that data
                BTTask_RemoveBlackboardData removeBlackboardData = new BTTask_RemoveBlackboardData(this, "LastSeenLocation");
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
                BlackboardDecorator checkLastSeenLocationDecorator = new BlackboardDecorator(this,
                    CheckLastSeenLocation,
                    "LastSeenLocation",
                    BlackboardDecorator.RunCondition.KeyExists,
                    BlackboardDecorator.NotifyRule.RunConditionChange,
                    BlackboardDecorator.NotifyAbort.none
                    );
            #endregion

            //ADD THE DECORATOR TO THE ROOTSELECTOR
            rootSelector.AddChild(checkLastSeenLocationDecorator);

        #endregion

        #region Patrolling
        //create the sequencer
        Sequencer PatrollingSequence = new Sequencer();

        //create the task getNextpatrol and move there
        BTT_GetNextPatrolPoint getNextPatrolPoint = new BTT_GetNextPatrolPoint(this, "PatrolPoint");
        BTTask_MoveToLocation moveToLocation = new BTTask_MoveToLocation(this, "PatrolPoint");
        //task to wait
        BTTask_Wait waitAtPatrolPoint = new BTTask_Wait(2f);

        //add the childs
        PatrollingSequence.AddChild(getNextPatrolPoint);
        PatrollingSequence.AddChild(moveToLocation);
        PatrollingSequence.AddChild(waitAtPatrolPoint);

        //we add the sequence on the selector
        rootSelector.AddChild(PatrollingSequence);
        #endregion

        //rootNode
        rootNode = rootSelector;
    }


}
