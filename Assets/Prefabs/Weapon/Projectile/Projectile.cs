using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, ITeamInterface
{
    //height or verticalDistance
    [SerializeField] float VerticalDistance;
    [SerializeField] Rigidbody rigidbody; 

    int TeamID = -1;

    //know the gameObject that shoot to know the team and the destination of the projectile
    //shoot the projectile in curve
    public void Launch(GameObject instigator, Vector3 destination)
    {
        //get what team is the gameObject that shoots the projectile
        ITeamInterface instigatorTeamInterface = instigator.GetComponent<ITeamInterface>();
        //if team exists, gameObject exists
        if(instigatorTeamInterface != null )
        {
            //we put the team of the projectile
            TeamID = instigatorTeamInterface.GetTeamID();
        }

        //calculate the time that last the projectil to reach the maximum height
        //the formula to calculate time it comes from the formula of the vertical movement--> height = (1/2* gravity * (time^2)) --> time = Math.SQRT((2 * height)/ Gravity)
        
        //first gravity
        float gravity = Physics.gravity.magnitude;
        //time
        float halfFlightTime = Mathf.Sqrt((VerticalDistance * 2.0f)/ gravity);

        //destination where the projectil goes
        Vector3 DestinationVec = destination - transform.position;
        DestinationVec.y = 0;
        //calculate horizontalDistance
        float horizontalDist = DestinationVec.magnitude;

        //initial vertical velocity = viV = g * t
        float upSpeed = halfFlightTime * gravity;
        //initial horizontal velocity = viH = h * (totalTime)
        float forwardSpeed = horizontalDist /(2.0f * halfFlightTime);

        //combine the velocities in 1 vector

        //vertical velocity = vertical direction * vertical speed
        Vector3 verticalVelocity = Vector3.up * upSpeed;
        //horizontalVelocity = horizontal direction * horizontal speed
        Vector3 horizontalVelocity = DestinationVec.normalized * forwardSpeed;

        Vector3 flightVelocity = verticalVelocity + horizontalVelocity;

        //apply the force to the rigidbody of the projectil
        rigidbody.AddForce(flightVelocity, ForceMode.VelocityChange);
    }

    public int GetTeamID(int teamID) { return teamID; }
}
