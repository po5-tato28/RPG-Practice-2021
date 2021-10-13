using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    [SerializeField] Text hpText;
    [SerializeField] Text mpText;
    [SerializeField] Text expText;


    private void Start()
    {
        SetHpText(playerStats.commonStats.CurrentHp, playerStats.commonStats.MaxHp);
        SetMpText(playerStats.CurrentMp, playerStats.MaxMp);
        SetExpText(playerStats.CurrentExp, playerStats.MaxExp);
    }


    public void SetHpText(int currentHp, int maxHp)
    {
        string value = currentHp + "/" + maxHp;

        hpText.GetComponent<Text>().text = value;
    }

    public void SetMpText(int currentMp, int maxMp)
    {
        string value = currentMp + "/" + maxMp;

        mpText.GetComponent<Text>().text = value;
    }

    public void SetExpText(float currentExp, float maxExp)
    {
        float percent;
        if (currentExp == 0) percent = 0;
        else percent = (maxExp / currentExp);

        string value = currentExp + "/" + maxExp + " (" + percent + "%)";

        expText.GetComponent<Text>().text = value;
    }
}

