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
    [SerializeField] float downwardPushConstant = -4f;

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
        else
            AirHorizontalMovement();

        VerticalMovement();
    }

    //  Handles all horizontal movement
    private Vector3 movement;
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;
        float maxDistance = moveSpeed * Time.deltaTime;

        float dX= Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float dY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        movement = Vector3.ClampMagnitude(transform.forward * dY + transform.right * dX , maxDistance);

        controller.Move(movement);
    }

    private Vector3 movementToVelocity(Vector3 move)
    {
        return move / Time.deltaTime;
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Handles all vertical movement
    void VerticalMovement()
    {
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
        controller.Move(movement);
    }


}
