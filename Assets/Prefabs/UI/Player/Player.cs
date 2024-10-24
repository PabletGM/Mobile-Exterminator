using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed;
    Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        //we ask if there has been any update on stickValue
        //we suscribe the function  to the event
        moveStick.onStickValueUpdated += moveStickmoveStick;
    }

    void moveStickmoveStick(Vector2 inputValue)
    {
        //we update the value of moveInput on realTime of the inputValue of the joystick
        moveInput = inputValue;
    }

    // Update is called once per frame
    void Update()
    {
        //because the character controller uses the x and z to use it but the moveInput is a Vector2
        characterController.Move(new Vector3(moveInput.x,0,moveInput.y) * Time.deltaTime * moveSpeed);
    }
}
