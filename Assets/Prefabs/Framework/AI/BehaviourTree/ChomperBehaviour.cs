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

        //create the sequencer
        Sequencer Root = new Sequencer();
        //add childs
        Root.AddChild(log);
        Root.AddChild(waitTask);

        //say the rootNode that it is a Sequencer
        rootNode = Root;
    }
}
