using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    CanvasGroup canvasGroup;
    private bool isVisible = false;
    TextMeshProUGUI dialogueText;

    Queue<string> sentences;
    bool skipPressed = false;
    [SerializeField] float typeSpeed = 50f;
    private List<DialogueDetector> dialogueDetectors;
    private Queue<Dialogue> dialoguesQueue;
    private Dialogue currentDialogue;



    // Start is called before the first frame update
    void Start()
    {
        dialogueDetectors = new List<DialogueDetector>();
        dialoguesQueue = new Queue<Dialogue>();
        sentences = new Queue<string>();
        dialogueText = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        HideDialogueBox();
    }

    public void AddDialogueDetector(DialogueDetector dd)
    {
        if(!dialogueDetectors.Contains(dd))
        {
            dialogueDetectors.Add(dd);
        }
    }

    private void TriggerDialogueDetectors()
    {
        foreach(DialogueDetector dd in dialogueDetectors)
        {
            if(dd != null)
                dd.TriggerEvents(currentDialogue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
    }

    // Queues up all sentences from the DialogueTrigger that invoked it
    public void StartDialogue(Dialogue dialogue)
    {
        if ( dialoguesQueue.Count > 0 )
        {
            dialoguesQueue.Enqueue(dialogue);
            dialogue = dialoguesQueue.Dequeue();
        }
        
        currentDialogue = dialogue;

        ShowDialogueBox();
        this.enabled = true;

        foreach(string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }

        DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        if(skipPressed)
        {
            dialogueText.text = sentence;
            yield return null;
        }

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1/typeSpeed);
        }
    }

    private void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string toDisplay = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(toDisplay));
    }

    private void CheckPlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.E) && isVisible)
        {
            DisplayNextSentence();
        }

    }

    private void HideDialogueBox()
    {
        canvasGroup.alpha = 0;
        isVisible = false;
    }

    private void ShowDialogueBox()
    {
        canvasGroup.alpha = 1;
        isVisible = true;
    }

    void EndDialogue()
    {
        TriggerDialogueDetectors();
        //Debug.Log("**End of Conversation**");
        HideDialogueBox();

        if(dialoguesQueue.Count > 0)
        {
            StartDialogue(dialoguesQueue.Peek());
        }
    }
}
