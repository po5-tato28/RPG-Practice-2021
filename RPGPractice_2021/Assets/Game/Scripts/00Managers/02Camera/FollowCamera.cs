using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    Vector3 originalPos;

    // 카메라 흔들림 효과를 재생 중인지 체크   
    public bool IsOnShake { set; get; }


    private void LateUpdate()
    {
        // 흔들림 효과 재생 중일 땐 이동 / 회전 중지
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