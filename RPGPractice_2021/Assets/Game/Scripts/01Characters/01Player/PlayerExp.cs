using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public PlayerStats stats;
    [SerializeField] UIManager uiManaer;
    
    int cE; // current Exp
    public int CE => cE;

    int cL; // currentLevel;
    public int CL => cL;


    void Start()
    {
        cL = stats.StartLevel;
    }

    void Update()
    {
        if (cE >= stats.MaxExp)
        {
            ExpFull();
        }
    }


    private void OnEnable()
    {
        cE = stats.CurrentExp;
        cL = stats.CurrentLevel;
    }

    public float GetExpValue()
    {
        return ((float)cE / (float)stats.MaxExp);
    }

    public void TakeExp(int point)
    {
        cE = Mathf.Max(cE + point, 0);
    }

    public void ExpFull()
    {        
        cE = 0;
        cL = stats.LevelUp();

        uiManaer.SetLevelText();

        stats.commonStats.AddMaxHp(cL);
        stats.AddMaxMp(cL);
        stats.AddMaxExp(cL);
    }
}