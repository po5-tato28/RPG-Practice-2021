using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueTypeClass[] dialogueClasses;

    [System.Serializable]
    public class DialogueTypeClass
    {
        public int dialogueOrder;
        //public DialogueType dialogueType;
        public DialogueListClass[] dialogueList = null;
    }

    [System.Serializable]
    public class DialogueListClass
    {
        public string characterName;

        [TextArea(3, 10)]
        public string[] sentences;
    }

    // dictionary로 저장 < 로그 넘버, < 이름, 문장[] > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<int, List<string>> name = null;
    [SerializeField] Dictionary<int, List<string[]>> sentences = null;

    public string GetNames(int order, int startNum)
    {
        // 컨테이너를 초기화하는 메서드 호출
        SetupNameTable();

        // 전달받은 매개변수의 값을 임시변수 charName 배열에 저장
        string charName = name[order][startNum];

        if (charName == null)
        {
            return "null";
        }

        return charName;
    }

    // logue Num = 현재 타입의 몇번째 로그를 재생할건가?
    public string[] GetDialogues(int order, int startNum)
    {
        // 컨테이너를 초기화하는 메서드 호출
        SetupLogueTable();

        // 전달받은 매개변수의 값을 임시변수 logues 배열에 저장
        string[] logues = sentences[order][startNum];

        if(logues == null)
        {
            return null;
        }


        return logues;
    }


    void SetupNameTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (name != null) return;

        name = new Dictionary<int, List<string>>();

        var nameLookupTable = new List<string>();

        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (DialogueTypeClass dialouge in dialogueClasses)
        {            
            // type에 맞는 stat 클래스를 순회한다 (type 클래스가 stat 클래스를 포함한다)
            foreach (DialogueListClass dialogueValue in dialouge.dialogueList)
            {
                nameLookupTable.Add(dialogueValue.characterName);
            }

            name[dialouge.dialogueOrder] = nameLookupTable;
        }
    }

    void SetupLogueTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (sentences != null) return;

        sentences = new Dictionary<int, List<string[]>>();

        var logueLookupTable = new List<string[]>();

        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (DialogueTypeClass dialogue in dialogueClasses)
        {
            // type에 맞는 stat 클래스를 순회한다 (type 클래스가 stat 클래스를 포함한다)
            foreach (DialogueListClass dialogueValue in dialogue.dialogueList)
            {
                logueLookupTable.Add(dialogueValue.sentences);
            }

            sentences[dialogue.dialogueOrder] = logueLookupTable;
        }
    }

}