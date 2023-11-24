using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    public Transform respawnPoint; // The point where the player will be respawned
    public CameraController playerCameraController; // Reference to the camera controller script


    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position; // Move the player to the respawn point
            Vector3 resetVelocity = Vector3.zero; // Reset player's velocity
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = resetVelocity;
            }
            
            // Reset camera position and rotation
            if (playerCameraController != null)
            {
                playerCameraController.ResetCamera();
            }
        }
    }
}