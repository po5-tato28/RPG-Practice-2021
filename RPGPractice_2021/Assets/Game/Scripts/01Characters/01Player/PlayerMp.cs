using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMp : MonoBehaviour
{
    //public PlayerStats stats;

    int maxMp;
    int currentMp; 
    public int CurrentMp { get { return currentMp; } }

    private void OnEnable()
    {
        maxMp = GetInitialMp();
        currentMp = maxMp;
    }

    public int GetInitialMp()
    {
        return GetComponent<BaseStats>().GetStat(StatsType.Mp);
    }

    public float GetMpValue()
    {
        return ((float)currentMp / (float)GetInitialMp());
    }

    public void TakeMp(int point)
    {
        currentMp = Mathf.Max(currentMp - point, 0);
    }

    public void RecoverMp(int point)
    {
        currentMp = Mathf.Max(currentMp + point, 0);
    }
}
