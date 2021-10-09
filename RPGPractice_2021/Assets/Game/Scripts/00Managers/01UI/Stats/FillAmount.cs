using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmount : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnClickButton()
    {
        StartCoroutine(CoolTime(3f));
    }

    IEnumerator CoolTime(float cool)
    {
        print("쿨타임 코루틴 실행");
        button.interactable = false;

        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        print("쿨타임 코루틴 완료");
        button.interactable = true;
    }

}
