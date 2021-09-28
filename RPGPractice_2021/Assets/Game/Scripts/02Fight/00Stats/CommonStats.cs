using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Common")]
public class CommonStats : ScriptableObject
{
    [SerializeField] private int maxHP = 100;
    public int currentHP = 100;

    public int GetMaxHP()
    {
        return maxHP;
    }
}
