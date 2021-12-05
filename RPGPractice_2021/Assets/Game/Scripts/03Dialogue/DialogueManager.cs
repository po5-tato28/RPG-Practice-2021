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
        // �ٸ� ui ������
        SettingOtherUI();
         
        // ��ȭ ui �ѱ�
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.characterName;

        // Queue �ʱ�ȭ
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
        // npc ī�޶� ����
        trigger.GetCurrentNPC().DisableNpcCamera();

        // ����Ʈ �ޱ�
        trigger.GetCurrentNPC().AcceptQuestFromNpc();

        // npc �ִϸ��̼� ����
        trigger.GetCurrentNPC().GetComponent<Animator>().SetBool("Talking", false);

        // ui ���̱�
        SettingOtherUI();

        animator.SetBool("IsOpen", false);

        // npc�� dialogue ����
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
