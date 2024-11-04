using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//collection of children nodes
public abstract class Compositor : BTNode
{
    //list with all the nodes
   LinkedList<BTNode> children = new LinkedList<BTNode>();
    //the current node we are workig on
   LinkedListNode<BTNode> currentChild = null;

    public void AddChild(BTNode newChild)
    {
        children.AddLast(newChild);
    }

    protected override NodeResult Execute()
    {
        //if there is not any node
        if(children.Count == 0)
        {
            return NodeResult.Success;
        }

        currentChild = children.First;
        return NodeResult.InProgress;
    }

    protected BTNode GetCurrentChild()
    {
        return currentChild.Value;
    }

    //to go to next node on children LinkedList
    protected bool Next()
    {
        if(currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }
        //if last node
        return false;
    }

    protected override void End()
    {
        currentChild = null;
    }
}
