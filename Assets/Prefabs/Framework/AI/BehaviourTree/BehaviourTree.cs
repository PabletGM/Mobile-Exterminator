using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    BTNode Root;
    Blackboard blackboard = new Blackboard();

    //property
    public Blackboard Blackboard 
    { 
        get { return blackboard; } 
    }


    private void Start()
    {
        //construct tree
        ConstructTree(out Root);
        //put the sort priority of everything
        SortTree();
    }

    private void SortTree()
    {
        int priorityCounter = 0;
        Root.SortPriority(ref priorityCounter);

    }

    protected abstract void ConstructTree(out BTNode rootNode);
    

    void Update()
    {
        //calls the UpdateNode
        Root.UpdateNode();
    }
}
