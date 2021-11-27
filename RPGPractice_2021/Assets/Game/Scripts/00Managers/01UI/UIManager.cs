using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] BaseStats playerStats;
    [SerializeField] Text levelText;
    [SerializeField] GameObject pressKeyNotifyPanel;


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

    public void EnterPressKeyNotify()
    {
        pressKeyNotifyPanel.gameObject.GetComponent<Animator>().SetBool("Enable", true);
    }
    public void ExitPressKeyNotify()
    {
        pressKeyNotifyPanel.gameObject.GetComponent<Animator>().SetBool("Enable", false);
    }
}

