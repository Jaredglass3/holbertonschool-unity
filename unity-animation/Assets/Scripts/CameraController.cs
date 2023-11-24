using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target object (i.e., our player) that the camera should follow
    public bool rotateTargetWithCamera = true;
    public Vector3 offset = new Vector3(0, 2.5f, -5); // Default offset from the target
    public float rotationSpeed = 5; // Speed for rotating the camera

    private float pitch = 0; // Vertical rotation
    private float yaw = 0; // Horizontal rotation

    void Update()
    {
        HandleInput();
        UpdateCameraPosition();
    }

    void HandleInput()
    {
        // Capture mouse input
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Clamp the pitch (vertical rotation) to prevent the camera from flipping
        pitch = Mathf.Clamp(pitch, -45, 45);
    }

    void UpdateCameraPosition()
    {
        // Update camera rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);

        // Update camera position based on rotation and offset
        transform.position = target.position + transform.TransformVector(offset);
        
        if (rotateTargetWithCamera)
        {
            Vector3 playerRotation = new Vector3(0, yaw, 0);
            target.rotation = Quaternion.Euler(playerRotation);
        }

    }
    
    public void ResetCamera()
    {
        // Reset the pitch and yaw values
        pitch = 0;
        yaw = 0;

        // Update the camera's position and rotation immediately
        UpdateCameraPosition();
    }

}