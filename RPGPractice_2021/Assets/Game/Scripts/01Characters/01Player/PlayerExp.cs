using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    //public PlayerStats stats;
    //public CharacterStats playerStats;
    [SerializeField] UIManager uiManaer;
    
    int currentExp; // current Exp
    public int CurrentExp => currentExp;

    int currentLevel; // currentLevel;
    public int CurrentLevel => currentLevel;


    void OnEnable()
    {
        currentExp = 0;
        currentLevel = GetComponent<BaseStats>().GetLevel();
        
    }

    void Update()
    {
        if (currentExp >= GetInitialExp())
        {
            ExpFull();
        }
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
    }

    public void ExpFull()
    {        
        currentExp = 0;
        //currentLevel = stats.LevelUp();
        currentLevel = GetComponent<BaseStats>().CalculateLevel();

        uiManaer.SetLevelText();

        //stats.commonStats.AddMaxHp(currentLevel);
        //stats.AddMaxMp(currentLevel);
        //stats.AddMaxExp(currentLevel);
    }
}