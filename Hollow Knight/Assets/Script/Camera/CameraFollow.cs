using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public Vector2 minCamaraRange;
    [SerializeField] private Vector2 maxCamaraRange;
    [SerializeField] private float cameraMoveSpeed;

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x, minCamaraRange.x, maxCamaraRange.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCamaraRange.y, maxCamaraRange.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, cameraMoveSpeed);
    }

}
