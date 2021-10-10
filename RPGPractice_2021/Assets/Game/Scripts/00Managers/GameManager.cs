using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public PlayerStats _PlayerStats { get { return playerStats; } }

    int currentMp;
    public int CurrentMp { get { return currentMp; } }

    int maxMp;
    public int MaxMp { get { return MaxMp; } }

    float currentExp;
    public float CurrentExp { get { return currentExp; } }

    float maxExp;
    public float MaxExp { get { return maxExp; } }


    private void Awake()
    {
        currentMp = playerStats.currentMP;
        maxMp = playerStats.GetMaxMp();

        currentExp = playerStats.currentEXP;
        maxExp = playerStats.GetMaxExp();
    }
}
