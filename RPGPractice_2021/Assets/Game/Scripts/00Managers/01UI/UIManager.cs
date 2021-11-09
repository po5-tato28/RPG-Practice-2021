using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public BaseStats playerStats;
    [SerializeField] Text levelText;

    
    void Start()
    {
        SetLevelText();
    }

    public void SetLevelText()
    {
        levelText.text = "Lv." + playerStats.GetCurrentLevel();
    }
}

