using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCsController : MonoBehaviour
{
    private bool isPossibleToTalk= false;
    public bool IsPossibleToTalk { get{ return IsPossibleToTalk;} }

    public DialogueType dialogueType;

    // ¿Ã∫•∆Æ
    public UnityEvent readyToTriggerDialogue;
    public UnityEvent exitToTriggerDialogue;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            readyToTriggerDialogue.Invoke();
            isPossibleToTalk = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            exitToTriggerDialogue.Invoke();
            isPossibleToTalk = false;
        }
    }
}
