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
    [SerializeField] ItemType type;
    [SerializeField] int point;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            RelayPoint();
            Destroy(this.gameObject);
        }
    }

    // Relay = 전달하다
    void RelayPoint()
    {
        switch(type)
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
