using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComponent : MonoBehaviour
{
    //Array with Waypoints
    public Transform[] patrolPoints;
    //patrol point index
    int currentPatrolPointIndex = -1;

    public bool GetNextPatrolPoint(out Vector3 point)
    {
        point = Vector3.zero;
        //if it exist any patrol point
        if(patrolPoints.Length == 0)
        {
            return false;
        }
        //we add 1 patrol point index
        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        //we give the parameter point
        point = patrolPoints[currentPatrolPointIndex].position;
        return true; 
    }
}
