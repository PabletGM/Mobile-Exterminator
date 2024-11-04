using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        //creates a node task of wait of 2 secs
        //rootNode = new BTTask_Wait(2f);
        //creates a node task of saying something
        rootNode = new BTTask_Log("Enemy can have task logs");
    }
}