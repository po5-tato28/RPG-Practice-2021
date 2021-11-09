using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    //public PlayerStats stats;
    //public CharacterStats playerStats;
    [SerializeField] UIManager uiManager;
    
    int currentExp; // current Exp
    public int CurrentExp => currentExp;

    public event Action onTakeExp;

    void OnEnable()
    {
        currentExp = 0;
    }

    void Update()
    {
    }

    public int GetInitialExp()
    {
        return GetComponent<BaseStats>().GetStat(StatsType.ExpToLevelUp);
    }

    public float GetExpValue()
    {
        return ((float)currentExp / (float)GetInitialExp());
    }

    public void TakeExp(int point)
    {
        currentExp = Mathf.Max(currentExp + point, 0);

        onTakeExp();
        uiManager.SetLevelText();
    }
}