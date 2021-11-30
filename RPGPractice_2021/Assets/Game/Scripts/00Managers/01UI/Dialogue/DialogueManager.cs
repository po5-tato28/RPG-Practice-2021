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


    private void Start()
    {
        senteces = new Queue<string>();
    }

    internal void StartDialogue(DialogueContainer dialogue, int startNum = 0)
    {
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

        // npc의 dialogue 변경
        trigger.GetCurrentNPC().ChangeDialogueType();

        // npc 애니메이션
        trigger.GetCurrentNPC().GetComponent<Animator>().SetBool("Talking", false);

        animator.SetBool("IsOpen", false);
    }
}
