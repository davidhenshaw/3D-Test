using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook1 : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 64f;
    float xRotation = 0f;
    PlayerController playerController;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        //Hide cursor and lock it to the window
        //if( !UnityEngine.Debug.isDebugBuild )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
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

        //arrow.GetComponent<RectTransform>().Rotate(Vector3.forward * 50f * Time.deltaTime);

    }

    public void RestrictRotation(Vector3 worldSpaceVector)
    {
        // Normalize and project world space vector onto screenspace
        Vector2 screenVector = new Vector2(worldSpaceVector.normalized.x, worldSpaceVector.normalized.y);
        Vector2 arrowVector = new Vector2(arrow.transform.eulerAngles.x, arrow.transform.eulerAngles.y);

        float rotationAngle = Vector2.SignedAngle(Vector2.up, screenVector);

        arrow.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

}
