using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTaskGroup_Patrolling : BTTask_Group
{
    float acceptableDistance;
    public BTTaskGroup_Patrolling(BehaviourTree tree, float acceptableDistance = 1) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BTNode Root)
    {
        //create the sequencer
        Sequencer PatrollingSequence = new Sequencer();

        //create the task getNextpatrol and move there
        BTT_GetNextPatrolPoint getNextPatrolPoint = new BTT_GetNextPatrolPoint(tree, "PatrolPoint");
        BTTask_MoveToLocation moveToLocation = new BTTask_MoveToLocation(tree, "PatrolPoint", acceptableDistance);
        //task to wait
        BTTask_Wait waitAtPatrolPoint = new BTTask_Wait(2f);

        //add the childs
        PatrollingSequence.AddChild(getNextPatrolPoint);
        PatrollingSequence.AddChild(moveToLocation);
        PatrollingSequence.AddChild(waitAtPatrolPoint);

        Root = PatrollingSequence;
    }
}
