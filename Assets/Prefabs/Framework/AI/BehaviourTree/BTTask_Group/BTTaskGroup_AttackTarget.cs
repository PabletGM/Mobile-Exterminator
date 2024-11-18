using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTaskGroup_AttackTarget : BTTask_Group
{
    float moveAcceptableDistance = 2f;
    float rotationAcceptableRadius = 10f;
    public BTTaskGroup_AttackTarget(BehaviourTree tree, float moveAcceptableDistance = 2f, float rotationAcceptableRadius = 10f) : base(tree)
    {
        this.moveAcceptableDistance = moveAcceptableDistance;
        this.rotationAcceptableRadius = rotationAcceptableRadius;
    }

    protected override void ConstructTree(out BTNode Root)
    {
        #region Attack

        //create an attack sequencer
        Sequencer attackTargetSeq = new Sequencer();

        #region AddTaskMoveToTargetToAttackSequence

        //create task moveToTarget
        BTTask_MoveToTarget moveToTarget = new BTTask_MoveToTarget(tree, "Target", moveAcceptableDistance);
        //rotate towards the target
        BTTask_RotateTowardsTarget rotateTowardsTarget = new BTTask_RotateTowardsTarget(tree, "Target", rotationAcceptableRadius);
        //attack
        BTTask_AttackTarget attackTarget = new BTTask_AttackTarget(tree, "Target");


        //add it to the sequencer
        attackTargetSeq.AddChild(moveToTarget);
        attackTargetSeq.AddChild(rotateTowardsTarget);
        attackTargetSeq.AddChild(attackTarget);

        #endregion

        #region Adding DecoratorToSelector

        //THE DECORATOR INCLUDES the child attackTargetSequencer and his task moveToTarget

        //create decorator with some conditions, if Target exists
        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(tree,
            attackTargetSeq, "Target",
            BlackboardDecorator.RunCondition.KeyExists,
            BlackboardDecorator.NotifyRule.RunConditionChange,
            BlackboardDecorator.NotifyAbort.both);

        //the parameter we get out of the class is the decorator with the sequence and everything
        Root = attackTargetDecorator;

        #endregion

        #endregion
    }
}
