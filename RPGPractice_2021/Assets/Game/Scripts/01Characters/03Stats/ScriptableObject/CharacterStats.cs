using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Stats/Characters", order = 0)]
public class CharacterStats : ScriptableObject
{
    [SerializeField] CharacterTypeClass[] characterClasses = null;

    [System.Serializable]
    class CharacterTypeClass
    {
        public CharacterType characterType;
        // stat Ŭ����
        public StatsListClass[] stats = null;
    }

    [System.Serializable]
    class StatsListClass
    {
        public StatsType statsType;
        public int[] levels = null;
    }


    // =========================================

    // dictionary�� ���� < ĳ���� Ÿ��, < ����, ���� > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<CharacterType, Dictionary<StatsType, int[]>> lookupTable = null;

    // �ܺο��� ���� ����
    public int GetStats(StatsType stat, CharacterType type, int lv)
    {
        // �����̳ʸ� �ʱ�ȭ�ϴ� �޼��� ȣ��
        SetupTable();

        // ���޹��� �Ű������� ���� �ӽú��� levels �迭�� ����
        int[] levels = lookupTable[type][stat];

        // ���� levels�� ���̰� �Է¹��� lv���� ª����
        if (levels.Length < lv)
        {
            // 0�� ��ȯ�ϰ� ���� -> �߸��� ���̹Ƿ� ����ó��
            return 0;
        }

        // levels���� [lv-1] �ε����� ���� ��ȯ
        return levels[lv - 1] ;
    }



    public int GetLevels(StatsType stat, CharacterType type)
    {
        // �����̳ʸ� �ʱ�ȭ�ϴ� �޼��� ȣ��
        SetupTable();

        // ���޹��� �Ű������� ���� �ӽú��� levels �迭�� ����
        int[] levels = lookupTable[type][stat];

        // levels �迭�� ���̸� ��ȯ
        // ���� ���� = �迭�� ����... (stat�� key�̰� level�� value��.)
        return levels.Length;
    }


    void SetupTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (lookupTable != null) return;

        lookupTable = new Dictionary<CharacterType, Dictionary<StatsType, int[]>>();
        
        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (CharacterTypeClass character in characterClasses)
        {
            // stat�� �ӽ÷� ������ �����̳ʸ� �����
            var statLookupTable = new Dictionary<StatsType, int[]>();

            // type�� �´� stat Ŭ������ ��ȸ�Ѵ� (type Ŭ������ stat Ŭ������ �����Ѵ�)
            foreach (StatsListClass statsValue in character.stats)
            {
                // stat �����̳ʿ� stat�� ���� ������ ��(=levels)�� �����Ѵ�.
                statLookupTable[statsValue.statsType] = statsValue.levels;
            }

            // stat Ŭ���� ��ȸ�� ������ ��ü �����̳ʿ� type�� ���� stat �����̳ʸ� �����Ѵ�.
            lookupTable[character.characterType] = statLookupTable;
        }
    }
}
