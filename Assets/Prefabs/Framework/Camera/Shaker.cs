using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class Shaker : MonoBehaviour
{
    [SerializeField] float shakeMag = 0.1f;
    [SerializeField] float shakeDuration = 1.0f;
    [SerializeField] Transform shakeTransform;
    [SerializeField] float shakeRecoverySpeed = 10f;

    Coroutine ShakeCoroutine;
    bool isShaking;

    Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    //method that will shake the camera when damage is made.
    public void StartShake()
    {
        if(ShakeCoroutine == null)
        {
            isShaking = true;
            ShakeCoroutine = StartCoroutine(ShakeStarted());
        }
    }

    IEnumerator ShakeStarted()
    {
        yield return new WaitForSeconds(shakeDuration);
        isShaking = false;
        ShakeCoroutine = null;
    }

    private void LateUpdate()
    {
        ProcessShake();
    }

    void ProcessShake()
    {
        if(isShaking)
        {
            Vector3 ShakeAmt = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f))
                           * shakeMag;
        }
        else
        {
            shakeTransform.localPosition = Vector3.Lerp(shakeTransform.localPosition, originalPos, Time.deltaTime * shakeRecoverySpeed);
        }
    }
}
