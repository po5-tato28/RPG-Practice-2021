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

    // dictionary로 저장 < 캐릭터 타입, 값 > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<ItemType, int[]> lookupTable = null;

    // 단일 dictionary이므로 ItemType 하나만 전달 받는다
    // level 대신 rnk를 받는다. (아이템 랭크라는 뜻!)
    public int GetDatas(ItemType type, int rnk = 1)
    {
        SetupTable();

        // 전달받은 매개변수의 값을 임시변수 ranks 배열에 저장
        int[] ranks = lookupTable[type];

        // 만약 임시변수 ranks의 길이가 입력받은 rnk보다 짧으면
        if (ranks.Length < rnk)
        {
            // 0을 반환하고 종료 -> 잘못된 것이므로 예외처리
            return 0;
        }

        // ranks에서 [rnk-1] 인덱스의 값을 반환
        return ranks[rnk - 1];
    }

    void SetupTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (lookupTable != null) return;

        lookupTable = new Dictionary<ItemType, int[]>();

        // 이중 dictionary가 아니므로 itemLookupTalbe은 List로 선언한다
        var itemLookupTable = new List<int>();

        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (ItemTypeClass itemType in itemClasses)
        {
            foreach (ItemValueListClass itemValue in itemType.items)
            {
                // 값을 추가한다
                itemLookupTable.Add(itemValue.rank);
            }

            // 배열 형태로 값을 전달한다.
            lookupTable[itemType.itemType] = itemLookupTable.ToArray();
        }
    }

}
