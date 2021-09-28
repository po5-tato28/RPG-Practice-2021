using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthComponent = null;
    [SerializeField] Slider slider = null;

    void Update()
    {
        slider.value = healthComponent.GetHealthPoint();
    }
}