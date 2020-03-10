using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDetector : ObjectDetector
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void TriggerEvents()
    {
        if(destroyAfterTrigger)
            Destroy(gameObject);
    }
}
