using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        //create the tasks
            //wait
        BTTask_Wait waitTask = new BTTask_Wait(2f);
            //log
        BTTask_Log log = new BTTask_Log("Logging");


 //=====================================================================================       
        //create the sequencer, it will make the 2 of them
        Sequencer RootSeq = new Sequencer();
        //add childs
        RootSeq.AddChild(log);
        RootSeq.AddChild(waitTask);

        //say the rootNode that it is a Sequencer
        rootNode = RootSeq;
//======================================================================================

        //create the selector, it will make the 2 of them
        Selector RootSel = new Selector();
        //add childs
        RootSel.AddChild(log);
        RootSel.AddChild(waitTask);

//======================================================================================
        ////say the rootNode that it is a Sequencer
        //rootNode = RootSeq;

        //say the rootNode that it is a Selector
        rootNode = RootSel;



    }


}
