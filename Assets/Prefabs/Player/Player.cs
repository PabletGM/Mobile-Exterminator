using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick moveStick;
    [SerializeField] Joystick aimStick;

    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float TurnSpeed = 20f;
    
    Vector2 moveInput;
    Vector2 aimInput;

    //reference of the camera
    Camera mainCam;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        //we ask if there has been any update on stickValue
        //we suscribe the function  to the event
        moveStick.onStickValueUpdated += moveStickUpdated;
        aimStick.onStickValueUpdated += aimStickUpdated;

        //camera reference to the camera with MainCamera tag
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    private void aimStickUpdated(Vector2 inputVal)
    {
        aimInput = inputVal;
    }

    void moveStickUpdated(Vector2 inputValue)
    {
        //we update the value of moveInput on realTime of the inputValue of the joystick
        moveInput = inputValue;
    }

    //so this function can make the worldDirection to the independent input touch of the joysticks if it is called
    Vector3 StickInputToWorldDirection(Vector2 inputVal)
    {
        //find the right direction of the camera in the local orientation
        Vector3 rightDir = mainCam.transform.right;
        //find the up direction of the camera in the local axes
        //Cross = third vector which is perpendicular to the two input vectors, the vector localXAxis and the WorldYAxis
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);

        //because the character controller uses the x and z to use it but the moveInput is a Vector2
        //Also, you multiply the camera move
        //rightDir * moveInput.x multiplies the direction by the input value to control how much the character should move to the right (or left if negative).
        //upDir* moveInput.y adjusts the movement in the forward/ backward direction based on the input value.


        //(rightDir * moveInput.x) + (upDir * moveInput.y): This adds the movement in the right/left direction
        // Calculate movement direction based on joystick input
        Vector3 worldDirection = (rightDir * inputVal.x) + (upDir * inputVal.y);
        return worldDirection;
    }

    //if the aim joystick is touched, we calculate the aimDirection and the player rotation
    void AimDirectionRotationPlayer(Vector3 aimDirection)
    {
        //if we touch the aim joystick
        if (aimInput.magnitude != 0)
        {
            //calculate aimDirection
            aimDirection = StickInputToWorldDirection(aimInput);
        }

        //if there is aimDirection we calculate the rotation of the player
        if (aimDirection.magnitude != 0)
        {
            //0.5f(if it is 1 it is b, if it is 0 it is a)
            float turnLerpAlpha = TurnSpeed * Time.deltaTime;
            //tells the player to look where the aimDirection, a rotation smooth so lerp between a and b in 
            Quaternion targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), turnLerpAlpha);
            transform.rotation = targetRotation;
        }
    }

    //if the player moves to the right or left the camera controller rotates
    void CameraControllerRotationRightLeft()
    {
        //so if the player moves to the right or left we change the camera controller rotation
        if (moveInput.magnitude != 0)
        {
            //if you are not aiming
            if(aimInput.magnitude == 0)
            {
                if (cameraController != null)
                {
                    //the degrees you want that the camera moves
                    cameraController.AddYawInput(moveInput.x);
                }
            }
           
        }
    }

    void PerformMoveAim()
    {
        Vector3 moveDirection = StickInputToWorldDirection(moveInput);
        //if we dont touch the aim joystick, it looks where we are moving
        Vector3 aimDirection = moveDirection;

        //calculate aimDirection and RotationPlayer
        AimDirectionRotationPlayer(aimDirection);

        //and the forward/backward direction, creating a vector that represents the total movement based on both axes of the input.
        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);

       
    }
    // Update is called once per frame
    void Update()
    {
        PerformMoveAim();
        //if the player moves to the right or left the camera controller rotates
        CameraControllerRotationRightLeft();
    }

   
}
