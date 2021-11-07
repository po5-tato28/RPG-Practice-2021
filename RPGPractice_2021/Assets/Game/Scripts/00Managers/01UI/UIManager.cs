using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] Text levelText;

    
    void Start()
    {
        levelText.text = "Lv." + playerStats.StartLevel.ToString();
    }

    public void SetLevelText()
    {
        levelText.text = "Lv." + playerStats.CurrentLevel.ToString();
    }
}

