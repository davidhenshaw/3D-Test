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
        HorizontalMovement();
        VerticalMovement();
    }

    //  Handles all horizontal movement
    private void HorizontalMovement()
    {
        float moveSpeed = groundSpeed;


        float dX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float dZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 moveVector = transform.forward * dZ + transform.right * dX;

        controller.Move(moveVector);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Handles all vertical movement
    void VerticalMovement()
    {

        if(isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
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

}
