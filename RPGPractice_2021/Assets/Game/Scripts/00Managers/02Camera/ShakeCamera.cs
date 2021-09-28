using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;
    public static ShakeCamera Instance => instance;

    private float shakeTime;
    private float shakeIntensity;

    private FollowCamera followCam;


    public void Awake()
    {
        instance = this;

        followCam = GetComponent<FollowCamera>();
    }


    ///<summary>
    /// 외부에서 카메라 흔들림을 조작할 때 호출하는 메서드
    /// ex) OnShakeCamera(1);           => 1초간 0.1의 세기로 흔들림
    /// ex) OnShakeCamera(0.5f, 1);     => 0.5초간 1의 세기로 흔들림
    ///</summary>
    /// <param name="_shakeTime"> 카메라 흔들림 지속 시간
    /// <param name="_shakeIntensity"> 카메라 흔들림 세기 (값이 클 수록 세게)
    public void OnShakeCamera(float _shakeTime = 1.0f, float _shakeIntensity = 0.1f)
    {
        shakeTime = _shakeTime;
        shakeIntensity = _shakeIntensity;

        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");

        //StopCoroutine("ShakeByRotation");
        //StartCoroutine("ShakeByRotation");
    }

    /// <summary>
    /// 카메라를 shakeTime 동안 shakeIntensity의 세기로 흔드는 코루틴 함수
    /// </summary>
    private IEnumerator ShakeByPosition()
    {
        // 카메라 흔들림 효과 재생 시작
        followCam.IsOnShake = true;

        // 흔들리기 직전의 시작 위치 (흔들림 종료 후 돌아올 위치)
        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            // 특정 축만 변경하길 원하면 아래 코드 이용 (이동하지 않을 축은 0 값 사용)
            // float x = Random.Range(-1f, 1f);
            // float y = Random.Range(-1f, 1f);
            // float z = Random.Range(-1f, 1f);
            // transform.position = startPosition + new Vector3(x, y, z) * shakeIntensity;

            // 초기 위치로부터 구 범위(Size 1) * shakeIntensity의 범위 안에서 카메라 위치 변동
            transform.position = startPosition + UnityEngine.Random.insideUnitSphere * shakeIntensity;

            // 시간 감소
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        // 흔들리기 직전의 좌표 값으로 설정
        transform.position = startPosition;

        // 카메라 흔들림 효과 재생 종료
        followCam.IsOnShake = false;
    }

    /// <summary>
    /// 카메라를 shakeTime 동안 shakeIntensity의 세기로 흔드는 코루틴 함수
    /// </summary>
    private IEnumerator ShakeByRotation()
    {
        // 카메라 흔들림 효과 재생 시작
        followCam.IsOnShake = true;

        // 흔들리기 직전의 회전 값
        Vector3 startRotation = transform.eulerAngles;
        // 회전의 경우 shakeIntensity에 더 큰 값이 필요하기 때문에 변수로 만듦
        // (클래스 멤버 변수로 선언해 외부에서 조작 가능)
        float power = 10f;


        while (shakeTime > 0.0f)
        {
            // 회전하길 원하는 축만 지정해서 사용 (회전하지 않을 축은 0으로 설정)
            float x = 0; // Random.Range(-1f, 1f);
            float y = 0; // Random.Range(-1f, 1f);
            float z = UnityEngine.Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            // 시간 감소
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        // 흔들리기 직전의 회전 값으로 설정
        transform.rotation = Quaternion.Euler(startRotation);

        // 카메라 흔들림 효과 재생 종료
        followCam.IsOnShake = false;
    }
}
