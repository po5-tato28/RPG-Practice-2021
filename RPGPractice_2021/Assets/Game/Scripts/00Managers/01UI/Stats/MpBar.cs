using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MpBar : MonoBehaviour
{
    //[SerializeField] GameManager gameManager;
    //[SerializeField] UIManager uiManager;

    [SerializeField] PlayerMp playerMp = null;

    [SerializeField] Slider slider = null;
    [SerializeField] Text mpText;

    private void Start()
    {
        if (mpText != null)
        {
            SetMpText(playerMp.CurrentMp, playerMp.GetInitialMp());
        }
        //gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameManager>();
        //uiManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<UIManager>();
    }

    private void Update()
    {
        slider.value = playerMp.GetMpValue();

        if (mpText != null)
        {
            SetMpText(playerMp.CurrentMp, playerMp.GetInitialMp());
        }
    }

    public void SetMpText(int currentMp, int maxMp)
    {
        string value = currentMp + "/" + maxMp;

        mpText.GetComponent<Text>().text = value;
    }
}
