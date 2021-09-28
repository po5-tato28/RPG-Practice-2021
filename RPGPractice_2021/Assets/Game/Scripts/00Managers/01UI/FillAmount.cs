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
        print("��Ÿ�� �ڷ�ƾ ����");
        button.interactable = false;

        while (cool > 1.0f)
        {
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        print("��Ÿ�� �ڷ�ƾ �Ϸ�");
        button.interactable = true;
    }

}
