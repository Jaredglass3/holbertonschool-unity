using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 1.0F;
    public float gravity = 20.0f;
    private Rigidbody selfRigidbody;

    private bool canJump = true;
    public float sensitivity = 4.0f;
    public Transform childObject;

    private Vector3 direction = Vector3.zero;

    CharacterController controller;
    public Animator animator;

    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private bool isRunning; // Variable to track running state

    public AudioClip footstepsGrass;
    public AudioClip footstepsRock;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
        if (selfRigidbody == null)
        {
            selfRigidbody = gameObject.AddComponent<Rigidbody>();
        }
        // contoller of player
        controller = GetComponent<CharacterController>();

        // take animation that's on child component
        animator = childObject.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canJump)
        {
            // Detect running state based on movement keys
            isRunning = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            selfRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            animator.SetBool("isRunning", false);
            canJump = false;
        }

        if (gameObject.transform.position.y <= -20)
        {
            gameObject.transform.position = new Vector3(0, 50, 0);
            animator.SetBool("isFalling", true);
            animator.SetTrigger("LandImpactTrigger");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FallingFlatImpact") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetTrigger("ImpactToGettingUp");
        }
    }

    void FixedUpdate()
    {
        float horizontal = 0, vertical = 0;

        bool isMoving = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (isMoving && canJump)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isJumping", false);
        }
        else if (!isMoving && canJump)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
        }

        if (canJump)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            vertical = 1;
            childObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            vertical = -1;
            childObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
            childObject.transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
            childObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        direction = new Vector3(horizontal, 0, vertical).normalized;

        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;
        transform.position += direction * speed * Time.deltaTime;

        // Check if running and on the ground to play footstep sounds
        if (isRunning && canJump)
        {
            string surfaceType = GetSurfaceType(); // Implement GetSurfaceType() based on your method
            PlayFootstepSound(surfaceType);
        }
    }

    private void PlayFootstepSound(string surfaceType)
    {
        {
    Debug.Log("Playing footstep sound on surface: " + surfaceType);
    // ... Your existing code ...
}
        AudioClip footstepClip = null;

        if (surfaceType == "grass")
        {
            footstepClip = footstepsGrass;
        }
        else if (surfaceType == "rock")
        {
            footstepClip = footstepsRock;
        }

        if (footstepClip != null)
        {
            audioSource.clip = footstepClip;
            audioSource.Play();
        }
    }

   private string GetSurfaceType()
{
    RaycastHit hit;

    if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.5f))
    {
        Debug.Log("Hit object tag: " + hit.collider.tag);
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GrassLayer"))
        {
            return "grass";
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("RockLayer"))
        {
            return "rock";
        }
    }

    return "default";  // Change this to the default surface type.
}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
