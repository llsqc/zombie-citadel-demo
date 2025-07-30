using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetPos;
    public float bodyHeight;

    public float moveSpeed;
    public float turnSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotation;

    private void Update()
    {
        if (target == null)
            return;

        targetPos = target.position + target.forward * offsetPos.z;
        targetPos += Vector3.up * offsetPos.y;
        targetPos += target.right * offsetPos.x;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }
}