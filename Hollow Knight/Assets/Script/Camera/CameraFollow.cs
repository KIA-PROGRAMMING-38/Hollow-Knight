using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Vector3 offSet;

    private void Awake()
    {
        offSet = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position + offSet;
    }
}
