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

        GetComponent<BaseStats>().onLevelUp += RecoverMp;
    }

    private void OnDisable()
    {
        GetComponent<BaseStats>().onLevelUp -= RecoverMp;
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

    public void RecoverMp()
    {
        if (currentMp >= GetInitialMp()) return;

        maxMp = GetComponent<BaseStats>().GetStat(StatsType.Hp);
        //int recoverMp = GetComponent<BaseStats>().GetStat(StatsType.Mp);

        currentMp = Mathf.Max(currentMp, maxMp);
    }

    public void RecoverMp(int point = 0)
    {
        if (currentMp >= GetInitialMp()) return;

        int recoverMp = currentMp + point;

        currentMp = Mathf.Max(currentMp, recoverMp);
        if (recoverMp > maxMp) currentMp = maxMp;
    }
}
