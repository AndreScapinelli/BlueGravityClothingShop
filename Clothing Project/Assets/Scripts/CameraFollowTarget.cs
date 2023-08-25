using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target; 
    public float followSpeed = 5f; 
    public float minDistanceToCenter = 1f; 
    public float maxDistanceToUseLerp = 10f; 

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = target.position;
        Vector3 currentPosition = transform.position;

        Vector3 desiredPosition = targetPosition - (targetPosition - currentPosition).normalized * minDistanceToCenter;

        float distanceToTarget = Vector3.Distance(currentPosition, targetPosition);

        if (distanceToTarget <= maxDistanceToUseLerp)
        {
            Vector3 newPosition = Vector3.Lerp(currentPosition, desiredPosition, followSpeed * Time.deltaTime);

            newPosition = new Vector3(newPosition.x, newPosition.y, -5);

            transform.position = newPosition;
        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}
