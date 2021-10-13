using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player")]
public class PlayerStats : ScriptableObject
{
    public CommonStats commonStats = null;

    [SerializeField] private int maxMp = 100;
    public int MaxMp { get { return maxMp; } }
    [SerializeField] private int currentMp = 100;
    public int CurrentMp { get { return currentMp; } }


    [SerializeField] private int maxExp = 100;
    public int MaxExp { get { return maxExp; } }
    [SerializeField] private int currentExp = 0;
    public int CurrentExp { get { return currentExp; } }


    [SerializeField] private int currentLevel = 1;
    public int CurrentLevel { get { return currentLevel; } }


    public int GetMpValue()
    {
        return (currentMp / maxMp);
    }
    public int GetExpValue()
    {
        return (currentExp / maxExp);
    }
}
