using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody playerRigidbody;
    private Vector3 startPosition;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);

        if (transform.position.y < -10f)
        {
            ResetPlayerPosition();
        }

        if (Input.GetButtonDown("Jump"))
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ResetPlayerPosition()
    {
        transform.position = startPosition;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
    }
}
