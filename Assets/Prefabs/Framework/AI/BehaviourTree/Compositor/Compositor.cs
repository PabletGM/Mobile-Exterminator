using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (currentChild == null) return;
        //so we stop working the child task
        currentChild.Value.Abort();
        currentChild = null;
    }

    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        foreach(var child in children)
        {
            child.SortPriority(ref priorityConter);
        }
    }

    //get override on the compositor function
    public override BTNode Get()
    {
        //Does it have any current child? current task?
        if (currentChild == null)
        {
            // Does it have any task ?
            if (children.Count != 0)
            {
                //return value of te first child
                return children.First.Value.Get();
            }
            //returning this node
            else
            {
                return this;
            }
        }
        return currentChild.Value.Get();

    }

    //we initialize all the children nodes
    public override void Initialize()
    {
        base.Initialize();
        foreach (var child in children)
        {
            child.Initialize();
        }
    }
}
