using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardDecorator : Decorator
{
    //to check if the key that we are looking for in the 
    public enum RunCondition
    {
        KeyExists,
        KeyNotExists
    }

    public enum NotifyRule
    {
        //the condition changes when we are running a task
        RunConditionChange,
        //For example, we change targets, the key the same but different value
        KeyValueChange
    }

    //what do we abort?
    public enum NotifyAbort
    {
        none,
        self,
        lower,
        both
    }

    RunCondition runCondition;
    NotifyRule notifyRule;
    NotifyAbort notifyAbort;


    //constructor
    public BlackboardDecorator(BTNode child) : base(child)
    {
    }

   
}
