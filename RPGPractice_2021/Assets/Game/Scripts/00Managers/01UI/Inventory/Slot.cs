using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;
    int itemCount;

    [SerializeField] Item getItem; // 획득한 아이템
    //public Item GetItem { get { return getItem; } }

    [SerializeField] Image slotImage;    
    [SerializeField] Text countText;

    InventoryUI inventoryUi;


    private void Awake()
    {
        slotImage = transform.GetChild(0).GetComponent<Image>();
        countText = transform.GetChild(1).GetComponent<Text>();
        inventoryUi = FindObjectOfType<InventoryUI>();
    }

    public void AddItem(Item item)
    {
        getItem = item;

        slotImage.sprite = item.ItemImage;

        if (slotImage.gameObject.activeSelf == false)
            slotImage.gameObject.SetActive(true);      
    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount(int count)
    {
        itemCount += count;
        //countText.text = itemCount.ToString();

        //if (countText.gameObject.activeSelf == false)
        //    countText.gameObject.SetActive(true);

        //if (itemCount <= 0)
        //ClearSlot();
    }
    // 테스트


    // relay Point
    public void UseItem(int slotNum)
    {
        switch (getItem.ItemType)
        {
            case ItemType.Hp:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().RecoverHealth(getItem.Point);
                    Remove(slotNum);
                }
                break;
            case ItemType.Mp:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMp>().RecoverMp(getItem.Point);
                    Remove(slotNum);
                }
                break;
            case ItemType.Exp:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExp>().GainExp(getItem.Point);
                    Remove(slotNum);
                }
                break;
            default:
                break;
        }
    }

    private void Remove(int slotNum)
    {
        inventoryUi.itemCount.RemoveAt(slotNum);
        getItem = null;
        slotImage.gameObject.SetActive(false);
    }
}
