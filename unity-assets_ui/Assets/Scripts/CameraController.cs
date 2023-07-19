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
    public bool isInverted = false;
    public int Inverted = -1;

    private Scene OptionsScene;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("InvertYToggle"))
            isInverted = PlayerPrefs.GetInt("InvertYToggle") == 0 ? false : true;
        else
            isInverted = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (isInverted)
            Inverted = 1;
        if (!isInverted)
            Inverted = -1;

        offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        offset = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * Inverted * turnSpeed, Vector3.right) * offset;
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);

        Quaternion targetRotation = transform.rotation;
        targetRotation.x = 0;
        targetRotation.z = 0;
        player.rotation = targetRotation;
    }
}