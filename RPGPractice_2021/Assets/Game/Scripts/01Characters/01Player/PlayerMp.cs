using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMp : MonoBehaviour
{
    public PlayerStats stats;

    int cM; // current Mp
    public int CM { get { return cM; } }

    private void OnEnable()
    {
        cM = stats.MaxMp;
    }

    public float GetMpValue()
    {
        return ((float)cM / (float)stats.MaxMp);
    }

    public void TakeMp(int point)
    {
        cM = Mathf.Max(cM - point, 0);
    }

    public void RecoverMp(int point)
    {
        cM = Mathf.Max(cM + point, 0);
    }
}
