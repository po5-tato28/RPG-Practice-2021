using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player")]
public class PlayerStats : ScriptableObject
{
    public CommonStats commonStats = null;

    private int maxMP = 100;
    public int currentMP = 100;

    private float maxEXP = 100.0f;
    public float currentEXP = 0.0f;

    public int currentLevel = 1;

    public int GetMaxMp()
    {
        return maxMP;
    }

    public float GetMaxExp()
    {
        return maxEXP;
    }
}
