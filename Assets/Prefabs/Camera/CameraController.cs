using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTrans;
    [SerializeField] float TurnSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = followTrans.position;
    }

    public void AddYawInput(float amt)
    {
        //rotate the object on the Y axis the -amt degrees* TurnSpeed * smooth way
        transform.Rotate(Vector3.up, amt * Time.deltaTime * TurnSpeed);
    }
}
