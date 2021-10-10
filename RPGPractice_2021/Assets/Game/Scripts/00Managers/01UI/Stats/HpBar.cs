using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    [SerializeField] Health health = null;
    [SerializeField] Slider slider = null;


    void Update()
    {
        slider.value = health.GetHealthPoint();

        if (!uiManager) return;
        uiManager.SetHpText(health.CurrentHp, health.MaxHp);
    }
}