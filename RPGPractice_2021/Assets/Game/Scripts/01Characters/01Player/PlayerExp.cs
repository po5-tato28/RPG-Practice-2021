using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    static PlayerExp instance;
    public static PlayerExp GetInstance()
    {
        return instance;
    }

    int currentExp; // current Exp
    public int CurrentExp
    {
        get { return currentExp; }
        set { currentExp = value; }
    }

    public event Action onTakeExp;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void OnEnable()
    {
        currentExp = 0;
    }


    public int GetInitialExp()
    {
        return GetComponent<BaseStats>().GetStat(StatsType.ExpToLevelUp);
    }

    public float GetExpValue()
    {
        float temp = ((float)currentExp / (float)GetInitialExp());

        return temp;
    }

    public void GainExp(int point)
    {
        currentExp = Mathf.Max(currentExp + point, 0);

        onTakeExp();
    }
}