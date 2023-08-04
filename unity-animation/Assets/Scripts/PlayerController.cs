using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public float speed;
    public float groundDrag;
    private float horizontalInput;
    private float verticalInput;
    public float jumpHeight = 5f;
    public float gravityScale = 9.8f;
    public float rotationSpeed = 10f;
    [SerializeField]
    private bool isGrounded;
    private Transform modelTransform;
    private Vector3 initialOffset;
    private Quaternion targetRotation;
    private AnimatorStateInfo stateInfo;
    private Vector3 moveDirection;

    private void Start()
    {
        modelTransform = transform.GetChild(0);
        initialOffset = modelTransform.position - transform.position;
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, 1.25f);
        TakeInput();

        rb.drag = groundDrag;

        if (transform.position.y < -30)
        {
            transform.position = new Vector3(0, 30, 0);
            animator.SetTrigger("Falling");
        }
        else if (transform.position.y < 2 && stateInfo.IsName("Falling"))
        {
            animator.ResetTrigger("Falling");
            modelTransform.position = new Vector3(modelTransform.position.x, -2.7f, modelTransform.position.z);
        }
        else if (stateInfo.IsName("Falling Flat Impact"))
        {
            modelTransform.position = new Vector3(modelTransform.position.x, -1f, modelTransform.position.z);
        }

        animator.SetBool("FallOnGround", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            Jump();
        }

        animator.SetBool("IsJumping", !isGrounded);
        animator.SetBool("IsMoving", (Mathf.Abs(verticalInput) > 0) || (Mathf.Abs(horizontalInput) > 0));

        if (moveDirection != Vector3.zero)
            targetRotation = Quaternion.LookRotation(moveDirection);
    }


    private void FixedUpdate()
    {
        MovePlayer();
        if (!isGrounded)
            rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass * 2);
    }

    private void TakeInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
    }

    private void MovePlayer()
    {
        moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
        animator.SetBool("IsMoving", (Mathf.Abs(verticalInput) > 0) || (Mathf.Abs(horizontalInput) > 0));
        rb.AddForce(moveDirection * speed * 10f, ForceMode.Force);
        modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        modelTransform.position = transform.position + initialOffset;
    }

    private void Jump() => rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
}