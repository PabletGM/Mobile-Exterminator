using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSense : SenseComponent
{
    [SerializeField]
    float sightDistance = 5f;

    [SerializeField]
    float sightHalfAngle = 5f;

    [SerializeField]
    float eyeHeight = 1f;
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        float distance = Vector3.Distance(stimuli.transform.position, transform.position);
        //the enemy doesnt see us
        if(distance > sightDistance )
        {
            return false;
        }

        //lets calculate the half angle of vision of the enemy

        //forward vector enemy 
        Vector3 forwardDir = transform.forward;
        //stimuli direction (direccion del estimulo) = line between stimuli and enemy position
        Vector3 stimuliDir = (stimuli.transform.position - transform.position).normalized;
        //calculate angle between forwardDir and StimuliDir( angulo entre la linea del estimulo al enemigo y la linea de frente al enemigo
        //calculate the angle that would create the enemy with the stimulis and the forward vector
        float angleNeededToSight = Vector3.Angle(forwardDir, stimuliDir);
        //check if it is more than sightHalfAngle(the actual sight angle to detect)
        if (angleNeededToSight > sightHalfAngle)
        {
            //it doesnt see the player
            return false;
        }
        //Being blocked by a Wall? Check with Raycast
        //the initial vector it starts from the ground
        if(Physics.Raycast(transform.position + (Vector3.up * eyeHeight), stimuliDir, out RaycastHit hitInfo,sightDistance))
        {
            //check the info, if it is not the stimulis
            if(hitInfo.collider.gameObject != stimuli.gameObject)
            {
                //there is something in the middle
                return false;
            }
        }

        //if it is on sightHalfAngle and no blocking
        return true;

    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Vector3 drawCenter = transform.position + (Vector3.up * eyeHeight);
        Gizmos.DrawWireSphere(drawCenter,sightDistance);

        //vector that will rotate around the Y Axis, that can be applied
        //to the forward direction of our transform
        Vector3 leftLimitDir = Quaternion.AngleAxis(sightHalfAngle, Vector3.up) * transform.forward;
        Vector3 rightLimitDir = Quaternion.AngleAxis(-sightHalfAngle, Vector3.up) * transform.forward;

        //will draw the left line and right line
        Gizmos.DrawLine(drawCenter, drawCenter + (leftLimitDir * sightDistance));
        Gizmos.DrawLine(drawCenter, drawCenter + (rightLimitDir * sightDistance));
    }
}
