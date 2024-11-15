using System;
using System.Collections;
using UnityEngine;


public class Shaker : MonoBehaviour
{
    [SerializeField] float shakeMagnitude = 0.1f;
    [SerializeField] float shakeDuration = 1.0f;
    [SerializeField] Transform shakeTransform;
    [SerializeField] float shakeRecoverySpeed = 10f;

    Coroutine ShakeCoroutine;
    bool isShaking;

    Vector3 originalPos;
    private bool enableVibration = true;

    void Start()
    {
        //we save the originalPosition
        originalPos = transform.position;
    }

    //method that will shake the camera when damage is made.
    public void StartShake()
    {
        //if it doesnt exist
        if(ShakeCoroutine == null)
        {
            //give permission to shake
            isShaking = true;
            //startCoroutine
            ShakeCoroutine = StartCoroutine(ShakeStarted());
        }
    }

    //coroutine
    IEnumerator ShakeStarted()
    {
        //wait shakeDuration
        yield return new WaitForSeconds(shakeDuration);
        isShaking = false;
        ShakeCoroutine = null;

        // Añadimos vibración si es en móvil
        if (enableVibration && IsMobilePlatform())
        {
            VibrateDevice();
        }
    }

    private void LateUpdate()
    {
        //make processShake
        ProcessShake();
    }

    void ProcessShake()
    {
        //if it has permission to shake
        if(isShaking)
        {
            //calculates a randomVector * shakeMagnitude * (1 o -1) = we have a random shake at any direction
            Vector3 ShakeAmt = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value) * shakeMagnitude * (UnityEngine.Random.value > 0.5 ? -1 : 1);
            //we add it to the localPosition(position respected to the father)
            shakeTransform.localPosition += ShakeAmt;


        }
        else
        {
            shakeTransform.localPosition = Vector3.Lerp(shakeTransform.localPosition, originalPos, Time.deltaTime * shakeRecoverySpeed);
        }
    }

    // Método para vibrar el dispositivo
    void VibrateDevice()
    {
        Handheld.Vibrate(); // Método integrado de Unity para vibración  
    }

    // Método para detectar si estamos en un dispositivo móvil
    bool IsMobilePlatform()
    {
        return Application.isMobilePlatform;
    }
}
