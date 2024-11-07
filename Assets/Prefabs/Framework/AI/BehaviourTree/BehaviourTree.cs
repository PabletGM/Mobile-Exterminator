using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    BTNode Root;
    Blackboard blackboard = new Blackboard();
    IBehaviourTreeInterface BehaviourTreeInterface;
    //BTNode previousNode;

    //property
    public Blackboard Blackboard 
    { 
        get { return blackboard; } 
    }


    private void Start()
    {
        //looks for a component that has the interface like enemy for example to have a reference
        BehaviourTreeInterface = GetComponent<IBehaviourTreeInterface>();
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
        //calls the UpdateNode of the root
        Root.UpdateNode();
        //currentNode
        //BTNode currentNode = Root.Get();
        //if(previousNode != currentNode) 
        //{
        //    //change previousNode
        //    previousNode = currentNode;
        //    //say the currentNode
        //    Debug.Log($"current node changed to: {currentNode}");
        //}
    }

    public void AbortLowerThan(int priority)
    {
        BTNode currentNode = Root.Get();
        //if current node is lower in the BT than priority argument
        if (currentNode.GetPriority() > priority) 
        {
            //it aborts what is currently running
            Root.Abort();
        }
    }

    internal IBehaviourTreeInterface GetBehaviourTreeInterface()
    {
        return BehaviourTreeInterface;
    }
}
