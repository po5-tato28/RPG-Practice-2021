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

    // dictionary�� ���� < ĳ���� Ÿ��, �� > >
    // Dictionary < TKey,TValue >
    [SerializeField] Dictionary<ItemType, int[]> lookupTable = null;
    public void GetItem()
    {
        SetupTable();
    }

    void SetupTable()
    {
        // �̹� �����Ǿ������� �ٷ� ����������
        if (lookupTable != null) return;

        lookupTable = new Dictionary<ItemType, int[]>();
        var itemLookupTable = new List<int>();

        // foreach �ݺ����� ����� type Ŭ������ ��ȸ�Ѵ�
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
