using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        //create a selector
        Selector rootSelector = new Selector();
        //create an attack sequencer
        Sequencer attackTargetSeq = new Sequencer();
        //create task moveToTarget
        BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(this,"Target");
        //add it to the sequencer
        attackTargetSeq.AddChild(moveToTarget);
       
        //create decorator
        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
            attackTargetSeq,"Target",
            BlackboardDecorator.RunCondition.KeyExists,
            BlackboardDecorator.NotifyRule.RunConditionChange,
            BlackboardDecorator.NotifyAbort.both);

        //add decorator to the selector
        rootSelector.AddChild(attackTargetDecorator);

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

    //create the tasks
    //            //wait
    //        BTTask_Wait waitTask = new BTTask_Wait(2f);
    //            //log
    //        BTTask_Log log = new BTTask_Log("Logging");
    //        //fail
    //        BTTask_AlwaysFail fail = new BTTask_AlwaysFail();

    // //=====================================================================================       
    //        //create the sequencer, it will make the 2 of them
    //        Sequencer RootSeq = new Sequencer();
    //        //add childs
    //        RootSeq.AddChild(fail);
    //        RootSeq.AddChild(log);
    //        RootSeq.AddChild(waitTask);
    ////======================================================================================

    //        //create the selector, it will make the 2 of them
    //        Selector RootSel = new Selector();
    //        //add childs
    //        RootSel.AddChild(fail);
    //        RootSel.AddChild(log);
    //        RootSel.AddChild(waitTask);

    //        //======================================================================================
    //        ////say the rootNode that it is a Sequencer
    //        rootNode = RootSeq;

    //say the rootNode that it is a Selector
    //rootNode = RootSel;


}
