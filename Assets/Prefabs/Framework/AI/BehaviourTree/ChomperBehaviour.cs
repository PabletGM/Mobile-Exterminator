using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(this, "Target");
        rootNode = moveToTarget;
            


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
