using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player")]
public class PlayerStats : ScriptableObject
{
    public CommonStats commonStats = null;

    [SerializeField] private int startLevel = 1;
    public int StartLevel => startLevel;

    [SerializeField] private int currentLevel = 1;
    public int CurrentLevel => currentLevel;


    [SerializeField] private int maxMp = 100;
    public int MaxMp => maxMp;
    [SerializeField] private int currentMp = 100;
    public int CurrentMp => currentMp; 


    [SerializeField] private int maxExp = 100;
    public int MaxExp => maxExp;
    [SerializeField] private int currentExp = 0;
    public int CurrentExp => currentExp; 
        

    public int GetMpValue()
    {
        return (currentMp / maxMp);
    }
    public int GetExpValue()
    {
        return (currentExp / maxExp);
    }

    // ·¹º§¾÷ =================================
    public int LevelUp(int grade = 1)
    {
        return currentLevel = currentLevel + grade;
    }

    public int AddMaxMp(int level)
    {
        int temp = level * 10;
        return maxMp = maxMp + temp;
    }

    public int AddMaxExp(int level)
    {
        int temp = level * 100;
        return maxExp = maxExp + temp;
    }
}
