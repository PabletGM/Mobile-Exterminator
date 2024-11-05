using System;
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

    string key;
    BehaviourTree tree;

    RunCondition runCondition;
    NotifyRule notifyRule;
    NotifyAbort notifyAbort;


    //constructor
    public BlackboardDecorator(BehaviourTree tree,
        BTNode child,
        string key,
        RunCondition runCondition,
        NotifyRule notifyRule,
        NotifyAbort notifyAbort) : base(child)
    {
        this.tree = tree;
        this.key = key;
        this.runCondition = runCondition;
        this.notifyRule = notifyRule;
        this.notifyAbort = notifyAbort;
    }

    protected override NodeResult Execute()
    {
        //see if it exists the blackboard
        Blackboard blackboard = tree.Blackboard;
        if(blackboard == null)
        {
            return NodeResult.Failure;
        }
        //add a method to the event onBlackboardValueChanged CheckNotify
        //if any blackboard changes
        blackboard.onBlackboardValueChange += CheckNotify;

        if(CheckRunCondition())
        {
            return NodeResult.InProgress;
        }
        else
        {
            return NodeResult.Failure;
        }
    }

    private bool CheckRunCondition()
    {
      throw new NotImplementedException();
    }

    private void CheckNotify(string key, object value)
    {
        
    }

    protected override NodeResult Update()
    {
        //will execute and update internally
        return GetChild().UpdateNode();
    }
}
