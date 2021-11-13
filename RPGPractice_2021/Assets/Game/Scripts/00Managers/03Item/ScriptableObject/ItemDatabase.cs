using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Item")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] ItemTypeClass[] itemClasses = null;

    [System.Serializable]
    class ItemTypeClass
    {
        public ItemType itemType;
        // item sprite
        public Sprite sprite;
        public ItemValueListClass[] items = null;
    }

    [System.Serializable]
    class ItemValueListClass
    {        
        public int rank;
    }

    // dictionary�� ���� < ĳ���� Ÿ��, �� > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<ItemType, int[]> lookupTable = null;

    // ���� dictionary�̹Ƿ� ItemType �ϳ��� ���� �޴´�
    // level ��� rnk�� �޴´�. (������ ��ũ��� ��!)
    public int GetDatas(ItemType type, int rnk = 1)
    {
        SetupTable();

        // ���޹��� �Ű������� ���� �ӽú��� ranks �迭�� ����
        int[] ranks = lookupTable[type];

        // ���� �ӽú��� ranks�� ���̰� �Է¹��� rnk���� ª����
        if (ranks.Length < rnk)
        {
            // 0�� ��ȯ�ϰ� ���� -> �߸��� ���̹Ƿ� ����ó��
            return 0;
        }

        // ranks���� [rnk-1] �ε����� ���� ��ȯ
        return ranks[rnk - 1];
    }

    void SetupTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (lookupTable != null) return;

        lookupTable = new Dictionary<ItemType, int[]>();

        // ���� dictionary�� �ƴϹǷ� itemLookupTalbe�� List�� �����Ѵ�
        var itemLookupTable = new List<int>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (ItemTypeClass itemType in itemClasses)
        {
            foreach (ItemValueListClass itemValue in itemType.items)
            {
                // ���� �߰��Ѵ�
                itemLookupTable.Add(itemValue.rank);
            }

            // �迭 ���·� ���� �����Ѵ�.
            lookupTable[itemType.itemType] = itemLookupTable.ToArray();
        }
    }

}
