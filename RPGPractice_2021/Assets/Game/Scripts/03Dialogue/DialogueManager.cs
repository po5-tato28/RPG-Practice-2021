using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> senteces;
    [SerializeField] DialogueTrigger trigger;

    [SerializeField] GameObject[] ui;


    private void Start()
    {
        senteces = new Queue<string>();
    }

    internal void StartDialogue(DialogueContainer dialogue, int startNum = 0)
    {
        // 다른 ui 가리기
        SettingOtherUI();
         
        // 대화 ui 켜기
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.characterName;

        // Queue 초기화
        senteces.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            senteces.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(senteces.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = senteces.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSenetenceCoroutine(sentence));
        //dialogueText.text = sentence;
    }

    private IEnumerator TypeSenetenceCoroutine (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        // npc 카메라 끄기
        trigger.GetCurrentNPC().DisableNpcCamera();

        // 퀘스트 받기
        trigger.GetCurrentNPC().AcceptQuestFromNpc();

        // npc 애니메이션 조정
        trigger.GetCurrentNPC().GetComponent<Animator>().SetBool("Talking", false);

        // ui 보이기
        SettingOtherUI();

        animator.SetBool("IsOpen", false);

        // npc의 dialogue 변경
        trigger.GetCurrentNPC().ChangeDialogueType();
    }

    private void SettingOtherUI()
    {
        for(int i = 0; i < ui.Length; i++)
        {
            if (ui[i].GetComponent<CanvasGroup>().alpha <= 0)
            {
                ui[i].GetComponent<CanvasGroup>().alpha = 1f;
            }

            else if (ui[i].GetComponent<CanvasGroup>().alpha > 0) 
            {
                ui[i].GetComponent<CanvasGroup>().alpha = 0f;
            }                
        }
    }
}
