using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GrabbableObject))]
public class GrabbableObjectListener : MonoBehaviour
{
    //Cached references
    GrabbableObject grabbaleParent;


    [Header("Events")]
    [SerializeField] protected GameEvent[] onDrop;
    [SerializeField] protected GameEvent[] onPickup;

    // Start is called before the first frame update
    public void Start()
    {
        grabbaleParent = GetComponent<GrabbableObject>();

        foreach(GameEvent et in onDrop)
        {
            grabbaleParent.onDropEvent += et.TriggerEvent;
        }

        foreach (GameEvent et in onPickup)
        {
            grabbaleParent.onPickUpEvent += et.TriggerEvent;
        }

    }

    // Update is called once per frame
    public void Update()
    {
        
    }

}
