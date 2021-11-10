using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<int> onSloutCountChange;

    private int slotCount;
    public int SlotCount
    {
        get => slotCount;
        set
        {
            slotCount = value;
            onSloutCountChange.Invoke(slotCount);
        }
    }

    private void Start()
    {
        slotCount = 4;
    }

}
