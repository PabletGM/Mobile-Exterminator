using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed;
    Vector2 moveInput;

    //reference of the camera
    Camera mainCam;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        //we ask if there has been any update on stickValue
        //we suscribe the function  to the event
        moveStick.onStickValueUpdated += moveStickmoveStick;
        //camera reference to the camera with MainCamera tag
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    void moveStickmoveStick(Vector2 inputValue)
    {
        //we update the value of moveInput on realTime of the inputValue of the joystick
        moveInput = inputValue;
    }

    // Update is called once per frame
    void Update()
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
        //and the forward/backward direction, creating a vector that represents the total movement based on both axes of the input.
        characterController.Move(( (rightDir *moveInput.x) + (upDir*moveInput.y) ) * Time.deltaTime * moveSpeed);
        
        //so if the player moves to the right or left we change the camera controller rotation
        if(cameraController!=null && moveInput.magnitude !=0)
        {
            //the degrees you want that the camera moves
            cameraController.AddYawInput(moveInput.x);
        }
    }
}
