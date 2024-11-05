using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardDecorator : Decorator
{
    //to check if the key that we are looking for exists or not
    public enum RunCondition
    {
        KeyExists,
        KeyNotExists
    }

    //notify any change
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
    object value;

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

        tree.Blackboard.onBlackboardValueChange -= CheckNotify;
        //add a method to the event onBlackboardValueChanged CheckNotify
        //if any blackboard changes
        blackboard.onBlackboardValueChange += CheckNotify;
        
        //check if exists the key
        if(CheckRunCondition())
        {
            return NodeResult.InProgress;
        }
        else
        {
            return NodeResult.Failure;
        }
    }

    //see if it exists the run condition key
    private bool CheckRunCondition()
    {
        bool exists = tree.Blackboard.GetBlackboardData(key, out value);
        switch(runCondition)
        {
            case RunCondition.KeyExists:
                return exists;
            case RunCondition.KeyNotExists:
                return !exists;
        }
        return false;
    }

    
    private void CheckNotify(string key, object val)
    {
        //the key has to be the same
        if(this.key != key)
        {
            return;
        }

        //the condition has changed
        if(notifyRule == NotifyRule.RunConditionChange)
        {
            //previous value
            bool previousExists = (this.value != null);
            //current value
            bool currentExists = (val != null);

            //compare, if there is change notify
            if(previousExists != currentExists)
            {
                Notify();
            }
        }
        //key value change
        else if(notifyRule == NotifyRule.KeyValueChange) 
        {
            //different value, notify
            if(this.value != val)
            {
                Notify();
            }
        }
    }

    //types of notify
    private void Notify()
    {
        switch(notifyAbort)
        {
            case NotifyAbort.none:
                break;
            case NotifyAbort.self:
                AbortSelf();
                break;
            case NotifyAbort.lower:
                AbortLower();
                break;
            case NotifyAbort.both:
                AbortBoth();
                break;
        }
    }

    private void AbortBoth()
    {
        Abort();
        AbortLower();
    }

    private void AbortLower()
    {

    }

    private void AbortSelf()
    {
        Abort();
    }

    protected override NodeResult Update()
    {
        //will execute and update internally
        return GetChild().UpdateNode();
    }

    protected override void End()
    {
       
        GetChild().Abort();
        base.End();
    }
}
