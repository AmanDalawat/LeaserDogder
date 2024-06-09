using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The player or object to follow
    public Vector3 offset; // Offset from the target object
    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement
    public float rotationSpeed = 5f; // Speed of camera rotation

    private void LateUpdate()
    {
        // Smoothly follow the target
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotate the camera around the target based on player input
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        Quaternion camTurnAngle = Quaternion.AngleAxis(horizontalInput * rotationSpeed, Vector3.up);
        offset = camTurnAngle * offset;

        Quaternion camPitchAngle = Quaternion.AngleAxis(-verticalInput * rotationSpeed, Vector3.right);
        offset = camPitchAngle * offset;

        transform.LookAt(target);
    }
}
