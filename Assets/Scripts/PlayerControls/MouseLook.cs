using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 64f;
    float xRotation = 0f;
    PlayerController playerController;
    [SerializeField] Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        //If the application tries to quit, release the mouse
        Application.quitting += ReleaseMouse;
        playerController = GetComponent<PlayerController>();
        CaptureMouse();
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Move();
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                CaptureMouse();
            }
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ReleaseMouse();
        }
    }

    private void Move()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;



        // Rotate the camera about its local x axis based on the mouseY
        xRotation -= mouseY;
        // rotation must be clamped so you can't look up and behind yourself
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Apply camera rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player about its local y axis based on the mouseX
        playerTransform.Rotate(Vector3.up * mouseX);
    }

    void CaptureMouse()
    {
        //Hide cursor and lock it to the window
        //if( !UnityEngine.Debug.isDebugBuild )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void ReleaseMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnMouseDown()
    {
        CaptureMouse();
    }

}
