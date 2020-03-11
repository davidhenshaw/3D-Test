using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableObject : MonoBehaviour
{
    protected Vector3 iVelocity;
    Vector3 prevPosition;
    protected Rigidbody myRigidbody;
    protected Collider myCollider;
    [SerializeField] AudioClip[] impactSounds;
    float impactSoundVolume = 0.85f;
    private bool isGrabbed;
    protected bool canHold = true;
    
    private float wallCollisionTimer = 0;
    private float wallCollisionTimeout = 0.15f;
    private float clipOverlapThreshold = 0.81f;

    protected int timesDropped;
    [Header("EventTriggers")]
    [SerializeField] protected GameEvent[] triggerOnDrop;
    [SerializeField] protected int[] triggerOnNthDrop;

    public event Action onDropEvent;
    public event Action onPickUpEvent;

    public int TimesDropped { get => timesDropped; }

    public abstract void OnGrab();

    public abstract void OnRelease();

    public abstract void OnThrow();

    public void Start()
    {
        prevPosition = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
    }

    float iVelocityLimit = 10f;
    public void Update()
    {
        Vector3 deltaPos = transform.position - prevPosition;
        iVelocity = deltaPos / Time.deltaTime;

        //if(iVelocity.magnitude > iVelocityLimit)
        //{
        //    canHold = false;
        //}

        //Debug.DrawLine(transform.position, transform.position + iVelocity, Color.blue);

        prevPosition = transform.position;
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }

    public bool CanHold()
    {
        return canHold;
    }

    private void MoveToGrabbableLayer()
    {
        // Move to a layer where player can collide with object again
        gameObject.layer = LayerMask.NameToLayer("Grabbable");
    }

    private void MoveToCarryingLayer()
    {
        //Move to a layer that cannot collide with player
        gameObject.layer = LayerMask.NameToLayer("PlayerCarrying");
    }

    public void Grab()
    {
        if(canHold)
        {
            MoveToCarryingLayer();

            isGrabbed = true;
            myRigidbody.useGravity = false;
            //myRigidbody.isKinematic = true;
            OnGrab();
        }
    }

    public void Release()
    {
        if (isGrabbed)
        {
            MoveToGrabbableLayer();

            isGrabbed = false;
            //myRigidbody.isKinematic = false;
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.useGravity = true;
        }
        OnRelease();
    }

    public void Throw(Vector3 throwVelocity)
    {
        if (isGrabbed)
        {
            MoveToGrabbableLayer();

            isGrabbed = false;
            //myRigidbody.isKinematic = false;
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.useGravity = true;
            myRigidbody.AddForce(throwVelocity+iVelocity, ForceMode.VelocityChange);
        }
        OnThrow();
    }

    public void SetCanHold(bool b)
    {
        canHold = b;   
    }

    public void MoveTo(Vector3 v)
    {
        float smoothness = 25f;

        myRigidbody.MovePosition(Vector3.Lerp(transform.position, v, Time.deltaTime * smoothness));
    }

    private float GetColliderOverlap(Collision collision)
    {
        ContactPoint[] contactPoints = new ContactPoint[collision.contactCount];
        float[] magnitudes = new float[collision.contactCount];

        collision.GetContacts(contactPoints);
        

        // get magnitudes of the vector between a contact point to the center of the game object
        for(int i = 0; i < magnitudes.Length; i++)
        {
            magnitudes[i] = (contactPoints[i].point - myCollider.bounds.center).magnitude;
            Debug.DrawLine(myCollider.bounds.center, contactPoints[i].point, Color.white);
        }
        

        float smallestMagnitude = Mathf.Min(magnitudes);
        
        return smallestMagnitude;
    }

    private void ProcessImpactSounds(Collision collision)
    {
        if(collision.impulse.magnitude > 0 )
        {
            int index = UnityEngine.Random.Range(0, impactSounds.Length - 1);
            AudioSource.PlayClipAtPoint(impactSounds[index], transform.position, impactSoundVolume);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(impactSounds != null)
        {
            ProcessImpactSounds(collision);
        }
    }

    // If this object overlaps too much with a wall, the player drops it automatically
    private void OnCollisionStay(Collision collision)
    {
        int otherLayer = collision.collider.gameObject.layer;

        Debug.DrawRay(myCollider.bounds.center, collision.impulse, Color.yellow);
        
        
        //if (otherLayer == LayerMask.NameToLayer("NoClip")       // If this object intersects environmental objects,
        //    || otherLayer == LayerMask.NameToLayer("Ground") 
        //    && isGrabbed) 
        //{
        //    /*Debug.Log("Overlap: " + GetColliderOverlap(collision));*/
        //    if(GetColliderOverlap(collision) < clipOverlapThreshold)
        //    {
        //        canHold = false;
        //    }

        //}

        
        //if (otherLayer == LayerMask.NameToLayer("NoClip") // If the object is overlaping a wall while not grabbed for <wallCollisionTimeout> seconds, the player can pick it up again
        //    || otherLayer == LayerMask.NameToLayer("Ground")
        //    && !isGrabbed)
        //{
        //    if (wallCollisionTimer < wallCollisionTimeout)
        //    {
        //        wallCollisionTimer += Time.deltaTime;
        //    }
        //    else
        //    {
        //        canHold = true;
        //        wallCollisionTimer = 0f;
        //    }
        //}
    }

    // As soon as this object leaves the wall it was stuck in, set canHold to true
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("NoClip") && !isGrabbed)
        {
            canHold = true;
        }
    }

   

}
