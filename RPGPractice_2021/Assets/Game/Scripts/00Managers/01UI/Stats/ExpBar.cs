using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] GameManager gameManager;    
    [SerializeField] UIManager uiManager;

    [SerializeField] Slider slider = null;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
        uiManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIManager>();
    }

    private void Update()
    {
        //SetSliderValue();
    }

    public void SetSliderValue()
    {
        slider.value = gameManager._PlayerStats.GetExpValue();
    }
}
