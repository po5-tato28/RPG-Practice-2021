using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Stats/Common")]
[Obsolete("Not use anymore.", true)]
public class CommonStats : ScriptableObject
{
    [SerializeField] private int maxHp = 100;
    public int MaxHp { get { return maxHp; } }

    [SerializeField] private int currentHp = 100;
    public int CurrentHp { get { return currentHp; } set { currentHp = value; } }


    // ·¹º§¾÷ =================================
    public int AddMaxHp(int level)
    {
        int temp = level * 20;
        return maxHp = maxHp + 20;
    }
}
