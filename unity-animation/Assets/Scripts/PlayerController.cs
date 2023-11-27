using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float airControlSpeed = 6f;
    public float jumpForce = 10.0f;
    public float gravity = 30.0f;
    public float fallThreshold = -10f; // Adjust the threshold based on your level design

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private Animator animator;  // Declare the animator variable

    public GameObject childObject;
    private Vector3 startingPosition;

    private bool isJumping = false;
    private bool isFalling = false;
    private bool hasLanded = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();  // Initialize the animator variable

        // Store the starting position for respawn
        startingPosition = transform.position;

        // Ensure the "FallingToImpact" trigger is initially false
        animator.SetBool("FallingToImpact", false);
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = (transform.forward * vertical) + (transform.right * horizontal);

        bool isMoving = direction.magnitude > 0.01f;

        animator.SetBool("IsRunning", isMoving);
        animator.SetBool("IsIdle", !isMoving);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);

        // Check if the player is falling beyond the threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
            return; // Skip the rest of the MoveCharacter logic
        }

        if (characterController.isGrounded)
        {
            moveDirection = direction * speed;

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                // Start the Jump animation immediately
                animator.SetTrigger("IdleToJump");
                isJumping = true;

                moveDirection.y = jumpForce;
            }

            // Check if the player has landed to trigger the Falling Flat Impact animation
            if (!hasLanded && characterController.isGrounded)
            {
                animator.SetBool("FallingToImpact", true);
                hasLanded = true;
            }
        }
        else
        {
            // Apply reduced air control
            moveDirection.x += direction.x * airControlSpeed * Time.deltaTime;
            moveDirection.z += direction.z * airControlSpeed * Time.deltaTime;

            if (!isFalling && moveDirection.y < 0)
            {
                // Start Falling animation when falling from a platform
                animator.SetTrigger("RunningToFalling");
                animator.SetTrigger("JumpToFalling");
                isFalling = true;

                // Reset landing variables when starting to fall
                hasLanded = false;
                animator.SetBool("FallingToImpact", false);  // Reset the trigger
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);

        // ... (existing code)

        // Declare and initialize child direction
        Vector3 childDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
            childObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
            childObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
            childObject.transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
            childObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        direction = new Vector3(horizontal, 0, vertical).normalized;
        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;
    }

    void Respawn()
    {
        // Implement respawn logic here
        // For example, reset the player's position to the starting position
        transform.position = startingPosition;

        // Reset other relevant variables or perform additional respawn actions
        isJumping = false;
        isFalling = false;

        // Optional: Add any other respawn-related logic here
    }
}
