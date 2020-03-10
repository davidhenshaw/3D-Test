using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueEvent")]
public class DialogueEvent : GameEvent
{
    [SerializeField] Dialogue dialogue;

    public override void TriggerEvent()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public Dialogue GetDialogue()
    {
        return dialogue;
    }

}
