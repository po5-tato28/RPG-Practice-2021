using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    //public static PlayerExp Instance;

    public PlayerStats stats;

    int cE; // current Exp
    public int CE { get { return cE; } }

    private void OnEnable()
    {
        cE = stats.CurrentExp;
    }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //}

    public float GetExpValue()
    {
        return ((float)cE / (float)stats.MaxExp);
    }

    public void TakeExp(int point)
    {
        cE = Mathf.Max(cE + point, 0);
    }
}
