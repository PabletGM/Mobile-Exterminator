using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//shared class to Movement things
public class MovementComponent : MonoBehaviour
{

//============================================================================================================================== Player USE
    [SerializeField] float turnSpeed = 16f;


    public float AimRotation(Vector3 aimDirection)
    {
        float currentTurnSpeed = 0;
        //if there is aimDirection we calculate the rotation of the player
        if (aimDirection.magnitude != 0)
        {
            Quaternion previousRotation = transform.rotation;

            //0.5f(if it is 1 it is b, if it is 0 it is a)
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            //tells the player to look where the aimDirection, a rotation smooth so lerp between a and b in 
            Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), turnLerpAlpha);
            transform.rotation = targetRotation;

            Quaternion currentRotation = transform.rotation;
            //calculate turn speed
            currentTurnSpeed = CalculateCurrentTurnSpeed(previousRotation, currentRotation, currentTurnSpeed, aimDirection);
        }
        return currentTurnSpeed;
       
    }

    float CalculateCurrentTurnSpeed(Quaternion previousRotation, Quaternion currentRotation, float currentTurnSpeed, Vector3 aimDirection)
    {
        //turnSpeed = rotationDelta / Time.deltaTime

        //the direction of the rotation, positive or negative
        //The dot product provides a value that helps determine the relative alignment of aimDirection and transform.right, positive,zero or negative
        // positive if aimDirection is aligned with transform.right
        // zero if they are perpendicular = 
        // negative if the aimDirection is in the opposite direction of transform.right
        //check if it is bigger than 0
        float Dir = Vector3.Dot(aimDirection, transform.right) > 0 ? 1 : -1;

        //First we get RotationDelta in degrees = angle between previous and currentRotation
        //how much we have rotated in the current frame
        float rotationDelta = Quaternion.Angle(previousRotation, currentRotation) * Dir;
        //turnSpeed = changeInRotation(rotationDelta) / timePassedSinceLastFrame(Time.deltaTime, vuelta de cada update)
        currentTurnSpeed = rotationDelta / Time.deltaTime;

        return currentTurnSpeed;
    }
    //============================================================================================================================== 
}
