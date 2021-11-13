using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventory;
    bool isActive = false;

    [SerializeField] List<Slot> slots;
    [SerializeField] Transform slotHolder;

    [SerializeField] ItemDatabase items;


    Inventory inven;

    private void Awake()
    {
        inven = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        inven.onSloutCountChange += SlotChange;
    }

    private void OnDisable()
    {
        inven.onSloutCountChange -= SlotChange;
    }

    void Start()
    {
        // Slot 리스트에 자식들을 전달하는 방법
        slots = new List<Slot>();        
        slotHolder.GetComponentsInChildren<Slot>(slots);

        if (inventory.activeSelf)
        {
            inventory.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActiveInventory();
        }
    }

    public void ActiveInventory()
    {
        isActive = !isActive;
        inventory.SetActive(isActive);
    }

    private void SlotChange(int count)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inven.SlotCount)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    //public void AddSlot()
    //{
    //    inven.SlotCount++;
    //}
}

