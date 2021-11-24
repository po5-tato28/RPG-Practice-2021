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

    // dictionary�� ���� < �α� Ÿ��, < �̸�, ����[] > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<DialogueType, List<string>> nameTable = null;
    [SerializeField] Dictionary<DialogueType, List<string[]>> dialogueTable = null;


    // logue Num = ���� Ÿ���� ���° �α׸� ����Ұǰ�?
    public string[] GetDialogues(DialogueType logueType, int logueNum)
    {
        // �����̳ʸ� �ʱ�ȭ�ϴ� �޼��� ȣ��
        SetupLogueTable();

        // ���޹��� �Ű������� ���� �ӽú��� logues �迭�� ����
        string[] logues = dialogueTable[logueType][logueNum];

        if(logues == null)
        {
            return null;
        }

        // levels���� [lv-1] �ε����� ���� ��ȯ
        return logues;
    }

    public string GetNames(DialogueType logueType, int logueNum)
    {
        // �����̳ʸ� �ʱ�ȭ�ϴ� �޼��� ȣ��
        SetupNameTable();

        // ���޹��� �Ű������� ���� �ӽú��� logues �迭�� ����
        string charName = nameTable[logueType][logueNum];

        if (charName.Length < logueNum)
        {
            return "null";
        }

        // levels���� [lv-1] �ε����� ���� ��ȯ
        return charName;
    }



    void SetupNameTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (nameTable != null) return;

        nameTable = new Dictionary<DialogueType, List<string>>();

        var nameLookupTable = new List<string>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (DialogueTypeClass dialogue in dialogueClasses)
        {            
            // type�� �´� stat Ŭ������ ��ȸ�Ѵ� (type Ŭ������ stat Ŭ������ �����Ѵ�)
            foreach (DialogueListClass dialogueValue in dialogue.dialogueList)
            {
                nameLookupTable.Add(dialogueValue.characterName);
            }

            nameTable[dialogue.dialogueType] = nameLookupTable;
        }
    }

    void SetupLogueTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (dialogueTable != null) return;

        dialogueTable = new Dictionary<DialogueType, List<string[]>>();

        var logueLookupTable = new List<string[]>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (DialogueTypeClass dialogue in dialogueClasses)
        {
            // type�� �´� stat Ŭ������ ��ȸ�Ѵ� (type Ŭ������ stat Ŭ������ �����Ѵ�)
            foreach (DialogueListClass dialogueValue in dialogue.dialogueList)
            {
                logueLookupTable.Add(dialogueValue.sentences);
            }

            dialogueTable[dialogue.dialogueType] = logueLookupTable;
        }
    }

}