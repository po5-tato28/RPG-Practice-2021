using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue[] dialogue;

    [SerializeField] NPCsController npcsController;

    public UnityEvent TryDialogue;
    public UnityEvent FinishDialogue;


    private void Update ()
    {
        KeyInput();
    }


    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //TriggerDialogue((int)DialogueType.FirstTime);
            TryDialogue.Invoke();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[1], npcsController.dialogueType);
    }
}
