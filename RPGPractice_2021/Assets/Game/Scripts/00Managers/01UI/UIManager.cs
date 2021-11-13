using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] BaseStats playerStats;

    [SerializeField] Text levelText;

    private void OnEnable()
    {
        playerStats.onLevelUp += SetLevelText;
    }
    private void OnDisable()
    {
        playerStats.onLevelUp -= SetLevelText;
    }


    void Start()
    {
        SetLevelText();
    }

    public void SetLevelText()
    {
        // m = 사용하지않음

        levelText.text = "Lv." + playerStats.GetCurrentLevel();
    }
}

