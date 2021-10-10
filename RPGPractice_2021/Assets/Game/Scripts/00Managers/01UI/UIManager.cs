using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Text mpText;
    [SerializeField] Text expText;

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
        float percent = (maxExp / currentExp) * 100;
        string value = currentExp + "/" + maxExp + " (" + percent + "%)";

        expText.GetComponent<Text>().text = value;
    }
}
