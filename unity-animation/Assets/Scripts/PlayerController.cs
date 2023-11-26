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

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private Animator animator;  // Declare the animator variable

    public GameObject childObject;

    private bool isJumping = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();  // Initialize the animator variable
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
        }
        else
        {
            // Apply reduced air control
            moveDirection.x += direction.x * airControlSpeed * Time.deltaTime;
            moveDirection.z += direction.z * airControlSpeed * Time.deltaTime;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);

        // Declare and initialize child direction
        Vector3 childDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
            // Assuming childObject is declared somewhere in the class
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

        // Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;

        // Check for landing to transition back to Idle or Running
        if (characterController.isGrounded && isJumping)
        {
            isJumping = false;

            if (isMoving)
            {
                // Start Running animation immediately after landing if still moving forward
                animator.SetTrigger("JumpToRunning");
            }
            else
            {
                // Start Idle animation immediately after landing if not moving forward
                animator.SetTrigger("JumpToIdle");
            }
        }
    }
}
