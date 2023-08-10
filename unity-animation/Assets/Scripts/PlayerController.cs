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

    }

    // void Update()
    // {
    //     float xDisplacement = Input.GetAxis("Horizontal");
    //     float zDisplacement = Input.GetAxis("Vertical");

    //     childObject.position = transform.position;
    //     childObject.rotation = transform.rotation;
    //     if (controller.isGrounded)
    //     {
    //         direction = new Vector3(xDisplacement * speed, 0, zDisplacement * speed);
    //         if (Input.GetKeyDown(KeyCode.Space))
    //         {
    //             direction.y = jumpSpeed;
    //             animator.SetTrigger("isJumping");
    //             animator.SetBool("isRunning", false);
    //             animator.SetBool("isIdle", false);
    //             canJump = false;
    //         }
    //     }
    //     else
    //         direction = new Vector3(xDisplacement * speed, direction.y, zDisplacement * speed);

    //     direction.y -= gravity * Time.deltaTime;

    //     if (direction.x != 0.0f || direction.z != 0.0f)
    //     {
    //         float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
    //         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
    //         transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //         Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    //         moveDir.y = direction.y;
    //         moveDir.x *= speed;
    //         moveDir.z *= speed;
    //         controller.Move(moveDir * Time.deltaTime);
    //         if (controller.isGrounded)
    //         {
    //             canJump = true;
    //             animator.SetBool("isRunning", true);
    //             animator.SetBool("isIdle", false);
    //         }
    //         else
    //         {
    //             animator.SetBool("isRunning", false);
    //             animator.SetBool("isIdle", false);
    //         }
    //     }
    //     else
    //     {
    //         controller.Move(direction * Time.deltaTime);
    //         if (controller.isGrounded)
    //         {
    //             canJump = true;
    //             animator.SetBool("isRunning", false);
    //             animator.SetBool("isIdle", true);
    //         }
    //         else
    //         {
    //             animator.SetBool("isRunning", false);
    //             animator.SetBool("isIdle", false);
    //         }
    //     }

    //     if (gameObject.transform.position.y <= -20)
    //         gameObject.transform.position = new Vector3(0, 15, 0);
    // }

    void Update()
    {
        // jump only when thouch ground
        // if (canJump)
        // {
        //     // if can jump mean that is not jumping
        //     animator.SetBool("isJumping", false);

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            selfRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            // direction.y = speed;
            // animator.SetTrigger("isJumping");
            animator.SetBool("isRunning", false);
            canJump = false;
        }

        // if player falls out map
        if (gameObject.transform.position.y <= -20)
        {
            gameObject.transform.position = new Vector3(0, 50, 0);
            animator.SetBool("isFalling", true);
             // Trigger the animation transition here:
        animator.SetTrigger("LandImpactTrigger");
    
        }
    }

    void FixedUpdate()
    {
        float horizontal = 0, vertical = 0;
        // direction = new Vector3(horizontal, 0, vertical).normalized;

        // check if some movement keys is beeing pressed
        bool isMoving = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ;

        // enable movement animations
        if (isMoving && canJump)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isJumping", false);
            // animator.SetTrigger("isJumping");
        }
        // if no movement keys is being pressed desable running animations
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
            // animator.SetTrigger("isJumping");
        }

        // four direction movement
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
        // Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        direction = new Vector3(horizontal, 0, vertical).normalized;

        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        // if is touching ground  
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}