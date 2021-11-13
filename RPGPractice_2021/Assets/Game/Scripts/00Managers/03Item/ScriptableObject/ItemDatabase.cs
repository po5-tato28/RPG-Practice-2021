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
        public ItemValueListClass[] itemList = null;
    }

    [System.Serializable]
    class ItemValueListClass
    {
        // item sprite
        public int itemNumber;
        public Sprite sprite;
        public int point;
    }

    // dictionary�� ���� < ĳ���� Ÿ��, �� > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<ItemType, Dictionary<int, int>> rankTable = null;
    [SerializeField] Dictionary<ItemType, Dictionary<int, Sprite>> spriteTable = null;

    // ���� dictionary�̹Ƿ� ItemType �ϳ��� ���� �޴´�
    // level ��� rnk�� �޴´�. (������ ��ũ��� ��!)
    public int GetItemValues(ItemType type, int num)
    {
        SetupTable();

        // ���޹��� �Ű������� ���� �ӽú��� ranks �迭�� ����
        int rank = rankTable[type][num];

        // ranks���� [rnk-1] �ε����� ���� ��ȯ
        return rank;
    }

    public Sprite GetItemSprites(ItemType type, int num)
    {
        SetupTable();

        // ���޹��� �Ű������� ���� �ӽú��� sprites �迭�� ����
        Sprite sprite = spriteTable[type][num];

        // ranks���� [rnk-1] �ε����� ���� ��ȯ
        return sprite;
    }


    void SetupTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (rankTable != null) return;

        rankTable = new Dictionary<ItemType, Dictionary<int, int>>();
        spriteTable = new Dictionary<ItemType, Dictionary<int, Sprite>>();

        // 
        var itemRankTable = new Dictionary<int, int>();
        var itemSpriteTable = new Dictionary<int, Sprite>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (ItemTypeClass item in itemClasses)
        {
            foreach (ItemValueListClass itemValue in item.itemList)
            {
                // ���� �߰��Ѵ�      
                itemRankTable.Add(itemValue.itemNumber, itemValue.point);
                itemSpriteTable.Add(itemValue.itemNumber, itemValue.sprite);
            }

            // �迭 ���·� ���� �����Ѵ�.
            rankTable[item.itemType] = itemRankTable;
            spriteTable[item.itemType] = itemSpriteTable;
        }
    }
}
