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


    //[Header("Object Manipulation")]
    //[SerializeField] GrabbableObject heldObject;
    //[SerializeField] float maxGrabDistance = 10f;
    //[SerializeField] LayerMask grabbablesMask;
    //[SerializeField] LayerMask obstructionsMask;
    //Vector3 holdObjectAtPosition;
    //[SerializeField] float holdDistance = 10f;
    //[SerializeField] Transform ghostArm;
    //[SerializeField] float throwStrength = 10f;

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
<<<<<<< HEAD
        if (isGrounded())
            HorizontalMovement();
        //else
        //    AirHorizontalMovement();

=======
        HorizontalMovement();
>>>>>>> parent of 03979047... Update player horizontal movement
        VerticalMovement();
    }

    //  Handles all horizontal movement
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;


        float dX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float dZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

<<<<<<< HEAD
        Vector3 moveDirection = transform.forward * yAxis + transform.right * xAxis;
        xzVelocity = moveDirection.normalized * moveSpeed;
=======
        Vector3 moveVector = transform.forward * dZ + transform.right * dX;
>>>>>>> parent of 03979047... Update player horizontal movement

        controller.Move(xzVelocity * Time.deltaTime);
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
<<<<<<< HEAD
            yVelocity.y = downwardPushConstant;
=======
            velocity.y = -2f;
>>>>>>> parent of 03979047... Update player horizontal movement
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

<<<<<<< HEAD
    void AirHorizontalMovement()
    {
        controller.Move(xzVelocity * Time.deltaTime);
    }

=======
>>>>>>> parent of 03979047... Update player horizontal movement
}
