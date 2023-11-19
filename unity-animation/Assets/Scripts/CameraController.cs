using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform player; // The target the camera should follow
    public float turnSpeed = 4.0f; // The speed at which the camera should follow the target
    public Vector3 offset; // The offset of the camera from the target
    public bool isInverted = false; // Whether the Y-axis movement is inverted
    public int Inverted = -1; // Multiplier for Y-axis movement based on inversion

    private Scene OptionsScene;

    // Start is called before the first frame update
    void Start()
    {
        // Check if there's a saved preference for inverting the Y-axis
        if (PlayerPrefs.HasKey("InvertYToggle"))
            isInverted = PlayerPrefs.GetInt("InvertYToggle") == 0 ? false : true;
        else
            isInverted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The Update method is empty in this script
        // It means there is no additional behavior during the frame update
    }

    void LateUpdate()
    {
        // LateUpdate is called after all Update functions have been called.
        // It's often used for camera manipulation to ensure the player position has been updated.

        // Set the Inverted variable based on the isInverted flag
        if (isInverted)
            Inverted = 1;
        if (!isInverted)
            Inverted = -1;

        // Update the camera offset based on mouse input
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * Inverted * turnSpeed, Vector3.right) * offset;

        // Set the camera position to follow the player with the calculated offset
        transform.position = player.transform.position + offset;

        // Make the camera look at the player
        transform.LookAt(player.transform.position);

        // Remove rotation around X and Z axes from the player
        Quaternion targetRotation = transform.rotation;
        targetRotation.x = 0;
        targetRotation.z = 0;
        player.rotation = targetRotation;
    }
}
