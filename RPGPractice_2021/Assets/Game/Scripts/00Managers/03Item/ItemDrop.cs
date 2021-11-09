using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    // 일단은 간단하게 구현하자~
    [SerializeField] List<GameObject> items;

    public void Drop()
    {
        int value = Random.Range(-1, items.Count);

        // -1이 나오면 빠져나간다 = 아이템이 드롭되지 않음
        if (value < 0) return; 

        Vector3 itemPos = transform.localPosition + new Vector3(0f, transform.localPosition.y + 1.0f, 0f);

        Instantiate(items[value], itemPos, Quaternion.Euler(-90, 0, 0));
    }
}
