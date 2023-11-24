using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float airControlSpeed = 6f;
    public float jumpForce = 10.0f;
    public float gravity = 30.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
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

        if(characterController.isGrounded)
        {
            moveDirection = direction * speed;

            if(Input.GetButtonDown("Jump"))
            {
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
    }

}
