using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{

    [Header("Object Manipulation")]
    [SerializeField] GrabbableObject heldObject;
    [SerializeField] float maxGrabDistance = 10f;
    [SerializeField] LayerMask grabbablesMask;
    [SerializeField] LayerMask obstructionsMask;
    Vector3 holdObjectAtPosition;
    [SerializeField] float holdDistance = 10f;
    [SerializeField] Transform ghostArm;
    [SerializeField] float throwStrength = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScaleArmBasedOnObstructions();

        if (heldObject != null)
        {
            CheckHeldObject();

            if (heldObject.CanHold())
                MoveHeldObject();
            else
                ReleaseHeldObject();
        }

        CheckPlayerInput();
    }

    // Attempts to grab the object in the player's current line of sight
    void GrabObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward * maxGrabDistance);
        RaycastHit hitInfo;
        Physics.Linecast(ray.origin, ray.origin + ray.direction * maxGrabDistance, out hitInfo, grabbablesMask);


        Color rayColor = Color.red;

        if (hitInfo.collider != null)
        {
            rayColor = Color.green;

            heldObject = hitInfo.collider.GetComponent<GrabbableObject>();
            heldObject.Grab();
        }


        Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxGrabDistance, rayColor);
    }

    void MoveHeldObject()
    {
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
        RaycastHit hitInfo;
        float minHoldDistance = 1f;

        if (Physics.Linecast(Camera.main.transform.position, targetPosition, out hitInfo, LayerMask.GetMask("Ground", "NoClip", "Default")))
        {
            float adjustedDistance = Mathf.Clamp(hitInfo.distance, minHoldDistance, holdDistance);
            targetPosition = Camera.main.transform.position + Camera.main.transform.forward * adjustedDistance;
        }

        heldObject.MoveTo(targetPosition);
    }

    // If the held object is too far away from 
    // where the player is trying to move it, drop the object
    float obstructionTimeout = 0.1f;
    float obstructionTimer = 0;
    void CheckHeldObject()
    {
        Ray lineOfSight = new Ray(Camera.main.transform.position, Camera.main.transform.forward * holdDistance);
        RaycastHit hitInfo = new RaycastHit();
        Color rayColor = Color.green;

        if (Physics.Linecast(lineOfSight.origin, heldObject.transform.position, out hitInfo, obstructionsMask))
        {
            Debug.Log("Line of Sight blocked by: " + hitInfo.collider.gameObject.name);
            rayColor = Color.red;
            if (obstructionTimer > obstructionTimeout)
            {
                ReleaseHeldObject();
            }

            obstructionTimer += Time.deltaTime;

        }
        Debug.DrawLine(lineOfSight.origin, lineOfSight.origin + lineOfSight.direction * holdDistance, rayColor);
    }

    private void ReleaseHeldObject()
    {
        if (heldObject != null)
        {
            heldObject.Release();
            heldObject = null;
        }
    }

    private void ThrowHeldObject()
    {
        if (heldObject != null)
        {
            Vector3 throwVelocity = Camera.main.transform.forward * throwStrength;
            heldObject.Throw(throwVelocity);
            heldObject = null;
        }
    }

    void CheckPlayerInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (heldObject == null)
            {
                GrabObject();
            }
            else
            {
                ReleaseHeldObject();
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            if (heldObject != null)
            {
                ThrowHeldObject();
            }
        }
    }

    private void ScaleArmBasedOnObstructions()
    {
        Ray lineOfSight = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        Physics.Linecast(lineOfSight.origin, lineOfSight.origin + lineOfSight.direction * 10f, out hitInfo, LayerMask.GetMask("Ground", "NoClip"));

        //Draw the normal of the surface we just hit
        if (hitInfo.collider != null)
        {
            Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.blue);
        }
    }
}
