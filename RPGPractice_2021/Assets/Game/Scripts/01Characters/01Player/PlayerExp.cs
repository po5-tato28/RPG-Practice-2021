using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public PlayerStats stats;

    int cE; // current Exp
    public int CE { get { return cE; } }

    private void OnEnable()
    {
        cE = stats.CurrentExp;
    }

    public float GetExpValue()
    {
        return ((float)cE / (float)stats.MaxExp);
    }

    public void GetExp(int point)
    {
        cE = Mathf.Max(cE + point, 0);
    }
}
