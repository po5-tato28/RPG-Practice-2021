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
        public int itemNumber;
        public Sprite sprite;
        public int point;
    }

    
    [SerializeField] Dictionary<ItemType, Dictionary<int, int>> rankTable = null;
    [SerializeField] Dictionary<ItemType, Dictionary<int, Sprite>> spriteTable = null;

    // ���� dictionary�̹Ƿ� ItemType �ϳ��� ���� �޴´�
    // level ��� rnk�� �޴´�. (������ ��ũ��� ��!)
    public int GetItemValues(ItemType type, int num)
    {
        SetupTable();

        // ���޹��� �Ű������� ���� �ӽú��� ranks �迭�� ����
        int rank = rankTable[type][num];

        return rank;
    }

    public Sprite GetItemSprites(ItemType type, int num)
    {
        SetupTable();

        // ���޹��� �Ű������� ���� �ӽú��� sprites �迭�� ����
        Sprite sprite = spriteTable[type][num];

        return sprite;
    }


    void SetupTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (rankTable != null) return;
        if (spriteTable != null) return;

        // �ʱ�ȭ
        rankTable = new Dictionary<ItemType, Dictionary<int, int>>();
        spriteTable = new Dictionary<ItemType, Dictionary<int, Sprite>>();

        // int (= itemNumber)�� Ű������ �ϴ� �ӽ� �����̳�
        var itemRankTable = new Dictionary<int, int>();
        var itemSpriteTable = new Dictionary<int, Sprite>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
        foreach (ItemTypeClass item in itemClasses)
        {
            foreach (ItemValueListClass itemValue in item.itemList)
            {
                // itemNumber�� key������, point�� sprite�� value�� �߰��Ѵ�      
                itemRankTable.Add(itemValue.itemNumber, itemValue.point);
                itemSpriteTable.Add(itemValue.itemNumber, itemValue.sprite);
            }

            // ���� �����Ѵ�.
            rankTable[item.itemType] = itemRankTable;
            spriteTable[item.itemType] = itemSpriteTable;
        }
    }
}
