using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : GrabbableObject, IMeltable
{
    float meltSpeed = 0.05f;
    // Ice cannot melt beyond this scale
    Vector3 minScale = Vector3.one * 0.5f;


    public void Melt()
    {
        if(transform.lossyScale.x > minScale.x)
        {
            // Scale down the object
            gameObject.transform.localScale -= Vector3.one * meltSpeed * Time.deltaTime;
        }
        //Debug.Log("Ice Scale Magnitude: " + transform.lossyScale.magnitude);
    }

    public override void OnGrab()
    {
        
    }

    public override void OnRelease()
    {

    }

    public override void OnThrow()
    {
        
    }

    private new void Update()
    {
        base.Update();
    }
}
