using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcController : MonoBehaviour
{
    [SerializeField] private bool isPossibleToTalk = false;
    [SerializeField] DialogueType dialogueType;

    [SerializeField] Camera npcCamera;

    //public int dialogueOrder;

    // �̺�Ʈ
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

    public bool GetIsPossibleToTalk()
    {
        return isPossibleToTalk;
    }


    public DialogueType GetNpcDialogueType()
    {
        return dialogueType;
    }

    // ī�޶� �ѱ�
    public void EnableNpcCamera()
    {
        if(!npcCamera.gameObject.activeSelf)
        {
            npcCamera.gameObject.SetActive(true);
        }
    }
    
    // ī�޶� ����
    public void DisableNpcCamera()
    {
        if (npcCamera.gameObject.activeSelf)
        {
            npcCamera.gameObject.SetActive(false);
        }
    }
}
