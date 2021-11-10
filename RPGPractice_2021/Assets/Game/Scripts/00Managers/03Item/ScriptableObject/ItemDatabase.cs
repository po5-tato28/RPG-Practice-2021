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
        public int value;
    }

    // dictionary로 저장 < 캐릭터 타입, 값 > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<ItemType, int[]> lookupTable = null;
    public void GetItem()
    {
        SetupTable();
    }

    void SetupTable()
    {
        // 이미 설정되어있으면 바로 빠져나간다
        if (lookupTable != null) return;

        lookupTable = new Dictionary<ItemType, int[]>();
        var itemLookupTable = new List<int>();

        // foreach 반복문을 사용해 type 클래스를 순회한다
        foreach (ItemTypeClass itemType in itemClasses)
        {
            foreach (ItemValueListClass itemValue in itemType.items)
            {
                itemLookupTable.Add(itemValue.value);
            }

            lookupTable[itemType.itemType] = itemLookupTable.ToArray();
            Debug.Log(itemLookupTable.ToArray());
        }
    }

}
