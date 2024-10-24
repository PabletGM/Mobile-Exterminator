using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//it always execute on play and editor mode
[ExecuteAlways]
public class CameraArm : MonoBehaviour
{
    //Follow the Player and Rotate the camera



    [SerializeField] float armLength;
    [SerializeField] Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //the more the armLength, the further the camera is
        child.position = transform.position - (child.forward * armLength);
    }

    //Draws a line between the 2 positions
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(child.position, transform.position);
    }
}
