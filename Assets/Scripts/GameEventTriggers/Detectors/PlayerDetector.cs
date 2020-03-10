using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : ObjectDetector
{
    public override void TriggerEvents()
    {
        if(destroyAfterTrigger)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        detectionDelay = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

}
