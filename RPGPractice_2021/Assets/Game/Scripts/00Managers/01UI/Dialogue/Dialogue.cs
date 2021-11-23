using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueTypeClass[] dialogueClass;

    [System.Serializable]
    public class DialogueTypeClass
    {
        public DialogueType dialogueType;
        public DialogueListClass[] dialogueList = null;
    }

    [System.Serializable]
    public class DialogueListClass
    {
        public string characterName;

        [TextArea(3, 10)]
        public string[] sentences;
    }
}

public enum DialogueType
{
    FirstTime,
    Citizen1,
    Citizen2,
    SecondTime,
    Knights,
}