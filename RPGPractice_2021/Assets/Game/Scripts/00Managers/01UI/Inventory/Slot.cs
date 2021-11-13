using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;
    int itemCount;

    [SerializeField] Item getItem; // »πµÊ«— æ∆¿Ã≈€
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
        Debug.Log(item.ToString());

        slotImage.sprite = item.ItemImage;
        Debug.Log(item.ItemImage.ToString());
        Debug.Log(slotImage.sprite.ToString());
        //countText.text = itemCount.ToString();

        if (slotImage.gameObject.activeSelf == false)
            slotImage.gameObject.SetActive(true);      
    }

    // «ÿ¥Á ΩΩ∑‘¿« æ∆¿Ã≈€ ∞πºˆ æ˜µ•¿Ã∆Æ
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
