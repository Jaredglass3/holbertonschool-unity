using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float cameraSpeed = 2f;
    public bool enableRotation = true;
    public float rotationSpeed = 2f;
    public Vector3 offset;

    public Transform startPosition; // Reference to the starting position

    private float mouseX;
    private float mouseY;

    public void start()
    {  // Check if the starting position is assigned
        if (startPosition == null)
        {
            Debug.LogWarning("Starting position not assigned in CameraController!");
            return;
        }

        // Set the target's position to the starting position
        target.position = startPosition.position;

        // Set the camera's position based on the starting position and offset
        transform.position = target.position + offset;
        }

    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target reference not set in CameraController!");
            return;
        }

        // Check if the player has fallen off the screen
        Vector3 screenPos = Camera.main.WorldToViewportPoint(target.position);
        if (screenPos.y < 0f)
        {
            // Reset the player to the starting position
            target.position = startPosition.position;
            return;
        }

        // Calculate the desired camera position
        Vector3 desiredPosition = target.transform.rotation * offset;
        desiredPosition += target.transform.position;
        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);

        // Camera rotation
        if (enableRotation)
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -60f, 60f); // Limit vertical rotation to avoid flipping

            // Rotate the camera around the target
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);
            transform.rotation = rotation;
            target.transform.rotation = Quaternion.Euler(Vector3.up * mouseX);
        }
    }
}
