using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    //[SerializeField] UIManager uiManager;
        
    [SerializeField] Health health = null;

    [SerializeField] Slider slider = null;
    [SerializeField] Text hpText;


    private void OnEnable()
    {
        
    }

    private void Start()
    {
        if (hpText != null)
        {
            SetHpText(health.CurrentHp, 100);
        }
        //uiManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIManager>();
    }

    private void Update()
    {
        slider.value = health.GetHpValue();
        if (hpText != null)
        {
            SetHpText(health.CurrentHp, 100);
        }
    }

    public void SetHpText(int currentHp, int maxHp)
    {
        string value = currentHp + "/" + maxHp;

        hpText.GetComponent<Text>().text = value;
    }

}