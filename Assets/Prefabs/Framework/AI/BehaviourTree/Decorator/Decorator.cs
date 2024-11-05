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
}
