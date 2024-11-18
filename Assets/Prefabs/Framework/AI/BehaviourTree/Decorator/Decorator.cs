using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : BTNode
{
    BTNode child;

    //getter
    protected BTNode GetChild()
    {
        return child;
    }

    public Decorator(BTNode child)
    {
        this.child = child;
    }
    public override void SortPriority(ref int priorityConter)
    {
        base.SortPriority(ref priorityConter);
        child.SortPriority(ref priorityConter);
    }

    //it gives the Get of the child(any compositor linked)
    public override BTNode Get()
    {
        return child.Get();
    }

    public override void Initialize()
    {
        base.Initialize();
        child.Initialize();
    }
}
