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
    Vector3 yVelocity = Vector3.zero;
    Vector3 xzVelocity = Vector3.zero;


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
        //    AirHorizontalMovement();

        VerticalMovement();
    }

    //  Handles all horizontal movement
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;


        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * yAxis + transform.right * xAxis;
        xzVelocity = moveDirection.normalized * moveSpeed;

        controller.Move(xzVelocity * Time.deltaTime);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Handles all vertical movement
    void VerticalMovement()
    {
        float downwardPushConstant = -4f; // Forces the player down to ensure small gaps won't de-ground the player

        if(isGrounded() && yVelocity.y < 0)
        {
            yVelocity.y = downwardPushConstant;
        }

        Vector3 initYVel = yVelocity;
        yVelocity.y += playerGravity * Time.deltaTime;

        HandleJump();

        controller.Move(yVelocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            yVelocity.y = (Mathf.Sqrt(-2 * jumpHeight * playerGravity));
        }
    }

    void AirHorizontalMovement()
    {
        controller.Move(xzVelocity * Time.deltaTime);
    }

}
