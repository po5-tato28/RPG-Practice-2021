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

    // dictionary�� ���� < �α� �ѹ�, < �̸�, ����[] > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<int, List<string>> name = null;
    [SerializeField] Dictionary<int, List<string[]>> sentences = null;

    public string GetNames(int order, int startNum)
    {
        // �����̳ʸ� �ʱ�ȭ�ϴ� �޼��� ȣ��
        SetupNameTable();

        // ���޹��� �Ű������� ���� �ӽú��� charName �迭�� ����
        string charName = name[order][startNum];

        if (charName == null)
        {
            return "null";
        }

        return charName;
    }

    // logue Num = ���� Ÿ���� ���° �α׸� ����Ұǰ�?
    public string[] GetDialogues(int order, int startNum)
    {
        // �����̳ʸ� �ʱ�ȭ�ϴ� �޼��� ȣ��
        SetupLogueTable();

        // ���޹��� �Ű������� ���� �ӽú��� logues �迭�� ����
        string[] logues = sentences[order][startNum];

        if(logues == null)
        {
            return null;
        }


        return logues;
    }


    void SetupNameTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (name != null) return;

        name = new Dictionary<int, List<string>>();

        var nameLookupTable = new List<string>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (DialogueTypeClass dialouge in dialogueClasses)
        {            
            // type�� �´� stat Ŭ������ ��ȸ�Ѵ� (type Ŭ������ stat Ŭ������ �����Ѵ�)
            foreach (DialogueListClass dialogueValue in dialouge.dialogueList)
            {
                nameLookupTable.Add(dialogueValue.characterName);
            }

            name[dialouge.dialogueOrder] = nameLookupTable;
        }
    }

    void SetupLogueTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (sentences != null) return;

        sentences = new Dictionary<int, List<string[]>>();

        var logueLookupTable = new List<string[]>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (DialogueTypeClass dialogue in dialogueClasses)
        {
            // type�� �´� stat Ŭ������ ��ȸ�Ѵ� (type Ŭ������ stat Ŭ������ �����Ѵ�)
            foreach (DialogueListClass dialogueValue in dialogue.dialogueList)
            {
                logueLookupTable.Add(dialogueValue.sentences);
            }

            sentences[dialogue.dialogueOrder] = logueLookupTable;
        }
    }

}