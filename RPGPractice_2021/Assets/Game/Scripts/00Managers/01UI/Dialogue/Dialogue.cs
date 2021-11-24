using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{
    FirstTime,
    Citizen1,
    Citizen2,
    SecondTime,
    Knights,
    ThirdTime,
}

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueTypeClass[] dialogueClasses;

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

    // dictionary로 저장 < 로그 타입, < 이름, 문장[] > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<DialogueType, List<string>> nameTable = null;
    [SerializeField] Dictionary<DialogueType, List<string[]>> dialogueTable = null;


    // logue Num = 현재 타입의 몇번째 로그를 재생할건가?
    public string[] GetDialogues(DialogueType logueType, int logueNum)
    {
        // 컨테이너를 초기화하는 메서드 호출
        SetupLogueTable();

        // 전달받은 매개변수의 값을 임시변수 logues 배열에 저장
        string[] logues = dialogueTable[logueType][logueNum];

        if(logues == null)
        {
            return null;
        }

        // levels에서 [lv-1] 인덱스의 값을 반환
        return logues;
    }

    public string GetNames(DialogueType logueType, int logueNum)
    {
        // 컨테이너를 초기화하는 메서드 호출
        SetupNameTable();

        // 전달받은 매개변수의 값을 임시변수 logues 배열에 저장
        string charName = nameTable[logueType][logueNum];

        if (charName.Length < logueNum)
        {
            return "null";
        }

        // levels에서 [lv-1] 인덱스의 값을 반환
        return charName;
    }



    void SetupNameTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (nameTable != null) return;

        nameTable = new Dictionary<DialogueType, List<string>>();

        var nameLookupTable = new List<string>();

        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (DialogueTypeClass dialogue in dialogueClasses)
        {            
            // type에 맞는 stat 클래스를 순회한다 (type 클래스가 stat 클래스를 포함한다)
            foreach (DialogueListClass dialogueValue in dialogue.dialogueList)
            {
                nameLookupTable.Add(dialogueValue.characterName);
            }

            nameTable[dialogue.dialogueType] = nameLookupTable;
        }
    }

    void SetupLogueTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (dialogueTable != null) return;

        dialogueTable = new Dictionary<DialogueType, List<string[]>>();

        var logueLookupTable = new List<string[]>();

        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (DialogueTypeClass dialogue in dialogueClasses)
        {
            // type에 맞는 stat 클래스를 순회한다 (type 클래스가 stat 클래스를 포함한다)
            foreach (DialogueListClass dialogueValue in dialogue.dialogueList)
            {
                logueLookupTable.Add(dialogueValue.sentences);
            }

            dialogueTable[dialogue.dialogueType] = logueLookupTable;
        }
    }

}