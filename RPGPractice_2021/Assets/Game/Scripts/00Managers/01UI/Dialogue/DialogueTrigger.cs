using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueList dialogue;

    [SerializeField] NPCsController npc;

    public UnityEvent TryDialogue;
    public UnityEvent FinishDialogue;


    private void Update ()
    {
        KeyInput();
        //if(npc.GetIsPossibleToTalk())

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
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue.containers[1], npc.dialogueOrder);
    }
}
