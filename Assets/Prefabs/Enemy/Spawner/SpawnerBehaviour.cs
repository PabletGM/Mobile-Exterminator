using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        
        //create the spawnTask
        BTTask_Spawn spawnTask = new BTTask_Spawn(this);
        //a cooldown decorator with the task
        CooldownDecorator spawnCooldownDecorator = new CooldownDecorator(this,spawnTask,10);
        //to make sure we have a target and add the decorator
        BlackboardDecorator spawnBBDecorator = new BlackboardDecorator(this,spawnCooldownDecorator,"Target",BlackboardDecorator.RunCondition.KeyExists, BlackboardDecorator.NotifyRule.RunConditionChange, BlackboardDecorator.NotifyAbort.both);

        rootNode = spawnBBDecorator;
        

        
    }

    
}
