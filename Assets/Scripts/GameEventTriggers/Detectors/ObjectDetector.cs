using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectDetector : MonoBehaviour
{
    private float currTime = 0;
    [SerializeField] protected float detectionDelay = 0.5f;
    bool isEventTriggered = false;
    [SerializeField] protected GameEvent[] eventTriggers;
    [SerializeField] string tagToDetect;
    [SerializeField] protected bool destroyAfterTrigger;

    [Header("Events")]
    [SerializeField] protected GameEvent[] onDrop;
    [SerializeField] protected GameEvent[] onPickup;



    public abstract void TriggerEvents();

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    // The item must remain in the trigger collider for at least <minFramesToDetect>
    public void OnTriggerStay(Collider other)
    {
        Collider myCollider = GetComponent<Collider>();

        if (other.tag.Equals(tagToDetect) && currTime >= detectionDelay && !isEventTriggered)
        {
            foreach(GameEvent et in eventTriggers)
            {
                et.TriggerEvent();
            }
            isEventTriggered = true;
            TriggerEvents();
        }
        else
        {
            currTime += Time.deltaTime;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(tagToDetect) && isEventTriggered)
        {
            ResetEventTrigger();
        }
    }

    public void ResetEventTrigger()
    {
        isEventTriggered = false;
        currTime = 0;
    }
}
