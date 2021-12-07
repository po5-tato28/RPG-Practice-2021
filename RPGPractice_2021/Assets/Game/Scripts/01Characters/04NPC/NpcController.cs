using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcController : MonoBehaviour
{
    [SerializeField] private bool isPossibleToTalk = false;
    public DialogueType dialogueType;

    [SerializeField] Camera npcCamera;

    //public int dialogueOrder;

    [SerializeField] GameObject questWindow;

    // 이벤트
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

    // 카메라 켜기
    public void EnableNpcCamera()
    {
        if(!npcCamera.gameObject.activeSelf)
        {
            npcCamera.gameObject.SetActive(true);
        }
    }
    
    // 카메라 끄기
    public void DisableNpcCamera()
    {
        if (npcCamera.gameObject.activeSelf)
        {
            npcCamera.gameObject.SetActive(false);
        }
    }

    public void ChangeDialogueType()
    {
        switch(dialogueType)
        {
            case DialogueType.First:
                dialogueType = DialogueType.Citizen1_1;
                break;
            case DialogueType.Second:
                dialogueType = DialogueType.Knight1_1;
                break;
            case DialogueType.Knight1_1:
                {
                    if(PlayerForQuest.instance.quest.isComplete)
                    {
                        dialogueType = DialogueType.Third;

                        PlayerExp.GetInstance().GainExp((int)PlayerForQuest.instance.quest.experienceReward);

                        questWindow.SetActive(false);
                    }
                    break;
                }
            case DialogueType.Third:
                dialogueType = DialogueType.Knight1_2;
                break;
            default:
                break;
        }
    }

    public void AcceptQuestFromNpc()
    {
        switch (dialogueType)
        {
            case DialogueType.Second:
                {
                    GetComponent<QuestGiver>().OpenQuestWindow();
                    GetComponent<QuestGiver>().AcceptQuest();
                }
                break;
            default:
                break;
        }        
    }
}
