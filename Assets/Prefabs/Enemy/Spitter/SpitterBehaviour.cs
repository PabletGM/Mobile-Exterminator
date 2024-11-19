using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//same behaviour of the chomper excepting the acceptableDistance = 5
public class SpitterBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        //create a selector
        Selector rootSelector = new Selector();

        #region Attack

        //creation of the attackTarget group
        BTTaskGroup_AttackTarget attackTargetGroupTask = new BTTaskGroup_AttackTarget(this, 5, 10f);
        //added as child
        rootSelector.AddChild(attackTargetGroupTask);

        #endregion

        #region CheckLastSeenLocationSequencer

        //creation of the  CheckLastSeenLocation group
        BTTaskGroup_MoveToLastSeenLocation CheckLastSeenLocation = new BTTaskGroup_MoveToLastSeenLocation(this, 1f);
        //added as child
        rootSelector.AddChild(CheckLastSeenLocation);

        #endregion

        #region Patrolling

        //creation of the  CheckLastSeenLocation group
        BTTaskGroup_Patrolling patrol = new BTTaskGroup_Patrolling(this, 1f);
        //added as child
        rootSelector.AddChild(patrol);

        #endregion

        //rootNode
        rootNode = rootSelector;
    }

   
}
