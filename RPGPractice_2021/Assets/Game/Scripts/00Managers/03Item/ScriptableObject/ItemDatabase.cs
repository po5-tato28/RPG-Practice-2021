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

    // dictionary로 저장 < 캐릭터 타입, 값 > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<ItemType, Dictionary<int, int>> rankTable = null;
    [SerializeField] Dictionary<ItemType, Dictionary<int, Sprite>> spriteTable = null;

    // 단일 dictionary이므로 ItemType 하나만 전달 받는다
    // level 대신 rnk를 받는다. (아이템 랭크라는 뜻!)
    public int GetItemValues(ItemType type, int num)
    {
        SetupTable();

        // 전달받은 매개변수의 값을 임시변수 ranks 배열에 저장
        int rank = rankTable[type][num];

        // ranks에서 [rnk-1] 인덱스의 값을 반환
        return rank;
    }

    public Sprite GetItemSprites(ItemType type, int num)
    {
        SetupTable();

        // 전달받은 매개변수의 값을 임시변수 sprites 배열에 저장
        Sprite sprite = spriteTable[type][num];

        // ranks에서 [rnk-1] 인덱스의 값을 반환
        return sprite;
    }


    void SetupTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (rankTable != null) return;

        rankTable = new Dictionary<ItemType, Dictionary<int, int>>();
        spriteTable = new Dictionary<ItemType, Dictionary<int, Sprite>>();

        // 
        var itemRankTable = new Dictionary<int, int>();
        var itemSpriteTable = new Dictionary<int, Sprite>();

        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (ItemTypeClass item in itemClasses)
        {
            foreach (ItemValueListClass itemValue in item.itemList)
            {
                // 값을 추가한다      
                itemRankTable.Add(itemValue.itemNumber, itemValue.point);
                itemSpriteTable.Add(itemValue.itemNumber, itemValue.sprite);
            }

            // 배열 형태로 값을 전달한다.
            rankTable[item.itemType] = itemRankTable;
            spriteTable[item.itemType] = itemSpriteTable;
        }
    }
}
