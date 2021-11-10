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
        public ItemListClass[] items = null;
    }

    [System.Serializable]
    class ItemListClass
    {        
        public int value;
    }
}
