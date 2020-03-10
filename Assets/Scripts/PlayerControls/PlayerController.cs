using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        //for(GameObject wheel in gameObject.GetComponentInChildren<>)
        myRigidBody.AddForce(Input.GetAxis("Accellerate") * transform.forward * moveSpeed);
    }

    private void Rotate()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            transform.Rotate(transform.up, 10 * Input.GetAxis("Horizontal"));
        }
    }

}
