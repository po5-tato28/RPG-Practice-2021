using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    [SerializeField] Health health = null;
    [SerializeField] Slider slider = null;

    private void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIManager>();
    }

    private void Update()
    {
        slider.value = health.GetHpValue();
    }

    //public void SetSliderValue() 
    //{
    //  slider.value = health.GetHpValue();
    //}

    public void SetSliderValue(int currentHp, int maxHp)
    {
        slider.value = (currentHp/maxHp);
    }
}