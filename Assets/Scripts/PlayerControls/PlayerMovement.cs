using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float groundSpeed = 9f;
    [SerializeField] float airSpeed = 2f;
    [SerializeField] float playerGravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] Vector3 velocity = Vector3.zero;

    // Cached references
    CharacterController controller;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
            HorizontalMovement();
       //else
       //     AirHorizontalMovement();

        VerticalMovement();
    }

    //  Handles all horizontal movement
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;


        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.forward * yAxis + transform.right * xAxis;

        moveVector = moveVector.normalized * moveSpeed * Time.deltaTime;

        controller.Move(moveVector);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Handles all vertical movement
    void VerticalMovement()
    {
        float downwardPushConstant = -4f; // Forces the player down to ensure small gaps won't de-ground the player

        if(isGrounded() && velocity.y < 0)
        {
            velocity.y = downwardPushConstant;
        }

        Vector3 initYVel = velocity;
        velocity.y += playerGravity * Time.deltaTime;

        HandleJump();

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            velocity.y = (Mathf.Sqrt(-2 * jumpHeight * playerGravity));
        }
    }

    void AirHorizontalMovement()
    {
        Vector3 moveVector = new Vector3(velocity.x, 0, velocity.z) * Time.deltaTime;

        controller.Move(moveVector);
    }

}
