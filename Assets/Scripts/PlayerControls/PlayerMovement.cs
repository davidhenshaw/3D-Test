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
    float downwardPushConstant = -2f;


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
        HorizontalMovement();
        VerticalMovement();
    }

    //  Handles all horizontal movement
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;
        float maxDistance = moveSpeed * Time.deltaTime;

        float dX= Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float dY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 movement = (transform.forward * dY + transform.right * dX);

        controller.Move(Vector3.ClampMagnitude(movement, maxDistance));
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
        controller.Move(xzVelocity * Time.deltaTime);
    }


}
