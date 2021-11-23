using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue[] dialogue;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerDialogue((int)DialogueType.FirstTime);
        }
    }

    public void TriggerDialogue(int dialogueType)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[dialogueType]);
    }
}
