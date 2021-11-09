using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    // �ϴ��� �����ϰ� ��������~
    [SerializeField] List<GameObject> items;

    public void Drop()
    {
        int value = Random.Range(-1, items.Count);

        // -1�� ������ ���������� = �������� ��ӵ��� ����
        if (value < 0) return; 

        Vector3 itemPos = transform.localPosition + new Vector3(0f, transform.localPosition.y + 1.0f, 0f);

        Instantiate(items[value], itemPos, Quaternion.Euler(-90, 0, 0));
    }
}
