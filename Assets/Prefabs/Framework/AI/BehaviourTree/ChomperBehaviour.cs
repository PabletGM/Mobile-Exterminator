using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        //creates a node task of wait of 2 secs
        rootNode = new BTTask_Wait(2f);
    }
}
