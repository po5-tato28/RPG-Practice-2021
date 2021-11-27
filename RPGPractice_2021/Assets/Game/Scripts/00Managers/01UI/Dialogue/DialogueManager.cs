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



    private void Start()
    {
        senteces = new Queue<string>();
    }

    internal void StartDialogue(Dialogue dialogue, DialogueType dialogueType, int logueNum = 0)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.GetNames(dialogueType, logueNum);

        // Queue √ ±‚»≠
        senteces.Clear();

        foreach (string sentence in dialogue.GetDialogues(dialogueType, logueNum))
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
        //Debug.Log("End of conversation.");
        animator.SetBool("IsOpen", false);
    }
}
