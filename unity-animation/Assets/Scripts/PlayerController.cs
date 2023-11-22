using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float jumpForce = 5f;
    private Rigidbody playerRigidbody;
    private Animator animator;
    private Vector3 startPosition;

    private int vertical = 0;
    private int horizontal = 0;

    public GameObject childObject;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component is null. Make sure it's properly assigned.");
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized * moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            playerRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime));
        }

        bool isMoving = movement.magnitude > 0.01f;

        animator.SetBool("IsRunning", isMoving);
        animator.SetBool("IsIdle", !isMoving);

        if (transform.position.y < -10f)
        {
            ResetPlayerPosition();
        }

        // Declare and initialize direction
        Vector3 direction = Vector3.zero;

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

        // Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        direction = new Vector3(horizontal, 0, vertical).normalized;

        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ResetPlayerPosition()
    {
        transform.position = startPosition;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
    }
}
