using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableObject : MonoBehaviour
{

    // Cached References
    protected Rigidbody myRigidbody;
    protected Collider myCollider;

    // State Variables
    protected Vector3 iVelocity;
    Vector3 prevPosition;

    [SerializeField] AudioClip[] impactSounds;
    float impactSoundVolume = 0.85f;

    private bool isGrabbed;
    protected bool canHold = true;
    protected int timesDropped;

    [Header("EventTriggers")]
    [SerializeField] protected GameEvent[] triggerOnDrop;
    [SerializeField] protected int[] triggerOnNthDrop;

    public event Action onDropEvent;
    public event Action onPickUpEvent;

    // Methods
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
