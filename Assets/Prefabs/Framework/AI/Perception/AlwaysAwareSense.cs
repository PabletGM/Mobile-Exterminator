using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAwareSense : SenseComponent
{
    //distance that the enemy detects
    [SerializeField] float awareDistance = 2f;

    //returns true or false if it is the stimuli of the player and the enemy close enough
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return Vector3.Distance(transform.position, stimuli.transform.position) <= awareDistance;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Gizmos.DrawWireSphere(transform.position + Vector3.up, awareDistance);
    }
}
