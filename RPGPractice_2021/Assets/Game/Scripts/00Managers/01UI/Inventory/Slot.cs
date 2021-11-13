using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;
    int itemCount;

    [SerializeField] Item getItem; // 획득한 아이템
    public Item GetItem { get { return getItem; } }

    [SerializeField] Image slotImage;    
    [SerializeField] Text countText;

    

    private void Awake()
    {
        slotImage = transform.GetChild(0).GetComponent<Image>();
        countText = transform.GetChild(1).GetComponent<Text>();
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
        countText.text = itemCount.ToString();

        if (countText.gameObject.activeSelf == false)
            countText.gameObject.SetActive(true);

        //if (itemCount <= 0)
        //ClearSlot();
    }
}
