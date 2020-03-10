using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDetector : MonoBehaviour
{
    DialogueManager dialogueManager;
    [SerializeField] GameEvent[] eventTriggers;
    [SerializeField] DialogueEvent dialogueToDetect;
    [SerializeField] bool destroyAfterTrigger;

    public void TriggerEvents(Dialogue dialogue)
    {
        if (dialogueToDetect.GetDialogue().Equals(dialogue))
        {
            foreach (GameEvent et in eventTriggers)
            {
                et.TriggerEvent();
            }

            if(destroyAfterTrigger)
            {
                if(gameObject != null)
                    Destroy(gameObject);
            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.AddDialogueDetector(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
