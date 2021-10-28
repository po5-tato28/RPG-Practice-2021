using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    //[SerializeField] GameManager gameManager;    
    //[SerializeField] UIManager uiManager;

    [SerializeField] PlayerExp playerExp = null;

    [SerializeField] Slider slider = null;
    [SerializeField] Text expText;

    private void Start()
    {
        SetExpText(playerExp.CE, playerExp.stats.MaxExp);
    }

    private void Update()
    {
        slider.value = playerExp.GetExpValue();
        //Debug.Log(playerExp.CE);

        if (expText != null)
        {
            SetExpText(playerExp.CE, playerExp.stats.MaxExp);
        }
    }

    public void SetExpText(float currentExp, float maxExp)
    {
        float percent;
        if (currentExp == 0) percent = 0;
        else percent = (currentExp / maxExp) * 100;

        string value = currentExp + "/" + maxExp + " (" + percent + "%)";

        expText.GetComponent<Text>().text = value;
    }
}
