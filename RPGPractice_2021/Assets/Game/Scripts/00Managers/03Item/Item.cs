using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Hp,
    Mp,
    Exp,
}

public class Item : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    public ItemType ItemType { get { return itemType; } }

    [SerializeField]
    [Range(10000, 20000)]
    int itemNumber;


    [SerializeField] ItemDatabase itemdata;

    [SerializeField] int point;

    [SerializeField] Sprite itemImage;
    public Sprite ItemImage { get { return itemImage; } }

    InventoryUI inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryUI>();
    }

    private void Start()
    {
        point = itemdata.GetItemValues(itemType, itemNumber);

        itemImage = itemdata.GetItemSprites(itemType, itemNumber);
    }

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
        switch(itemType)
        {
            case ItemType.Hp:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().RecoverHealth(point);
                    
                    inventory.GetItem(this);
                }
                break;
            case ItemType.Mp:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMp>().RecoverMp(point);

                    inventory.GetItem(this);
                }
                break;
            case ItemType.Exp:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExp>().GainExp(point);

                    inventory.GetItem(this);
                }
                break;
            default:
                break;
        }
    }
}
