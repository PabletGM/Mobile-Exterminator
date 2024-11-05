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
        ConstructTree(out Root);
    }

    protected abstract void ConstructTree(out BTNode rootNode);
    

    void Update()
    {
        //calls the UpdateNode
        Root.UpdateNode();
    }
}
