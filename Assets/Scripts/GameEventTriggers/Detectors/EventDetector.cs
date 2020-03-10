using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventDetector : MonoBehaviour
{
    [SerializeField] GameEvent[] eventTriggers;
    [SerializeField] protected bool destroyAfterTrigger;

    protected void TriggerEvents()
    {
        foreach (GameEvent et in eventTriggers)
        {
            et.TriggerEvent();
        }
    }

}
