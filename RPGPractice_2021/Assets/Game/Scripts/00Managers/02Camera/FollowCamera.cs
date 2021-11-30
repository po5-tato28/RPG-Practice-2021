using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    Vector3 originalPos;

    // ī�޶� ��鸲 ȿ���� ��� ������ üũ   
    public bool IsOnShake { set; get; }


    private void LateUpdate()
    {
        // ��鸲 ȿ�� ��� ���� �� �̵� / ȸ�� ����
        if (IsOnShake == true) return;

        LookAtCharacter();
    }

    private void LookAtCharacter()
    {
        transform.position = target.position;

        transform.LookAt(target);
    }

    public void SettingCamera()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

}