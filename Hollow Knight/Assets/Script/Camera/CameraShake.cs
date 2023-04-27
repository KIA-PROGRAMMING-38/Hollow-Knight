using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;
    private Vector3 cameraPos;

    [SerializeField]
    [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField]
    [Range(0.1f, 1f)] float duration = 0.5f;
    
    public void Shake()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }


    private void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 10 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 10 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    private void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }

}