using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Exp,
    Hp,
    Mp,
}

public class Item : MonoBehaviour
{
    [SerializeField] ItemType item;
    [SerializeField] ItemDatabase itemdata;
    [SerializeField] int point;

    private void Start()
    {
        itemdata.GetItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            RelayPoint();
            Destroy(this.gameObject);
        }
    }

    // Relay = �����ϴ�
    void RelayPoint()
    {
        switch(item)
        {
            case ItemType.Exp:
                {
                    point = 10;

                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExp>().TakeExp(point);
                }
                break;
            case ItemType.Hp:
                { 

                }
                break;
            case ItemType.Mp:
                {

                }
                break;
            default:
                break;
        }
    }
}
