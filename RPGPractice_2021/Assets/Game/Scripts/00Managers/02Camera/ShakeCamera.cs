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
    /// �ܺο��� ī�޶� ��鸲�� ������ �� ȣ���ϴ� �޼���
    /// ex) OnShakeCamera(1);           => 1�ʰ� 0.1�� ����� ��鸲
    /// ex) OnShakeCamera(0.5f, 1);     => 0.5�ʰ� 1�� ����� ��鸲
    ///</summary>
    /// <param name="_shakeTime"> ī�޶� ��鸲 ���� �ð�
    /// <param name="_shakeIntensity"> ī�޶� ��鸲 ���� (���� Ŭ ���� ����)
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
    /// ī�޶� shakeTime ���� shakeIntensity�� ����� ���� �ڷ�ƾ �Լ�
    /// </summary>
    private IEnumerator ShakeByPosition()
    {
        // ī�޶� ��鸲 ȿ�� ��� ����
        followCam.IsOnShake = true;

        // ��鸮�� ������ ���� ��ġ (��鸲 ���� �� ���ƿ� ��ġ)
        Vector3 startPosition = transform.position;

        while (shakeTime > 0.0f)
        {
            // Ư�� �ุ �����ϱ� ���ϸ� �Ʒ� �ڵ� �̿� (�̵����� ���� ���� 0 �� ���)
            // float x = Random.Range(-1f, 1f);
            // float y = Random.Range(-1f, 1f);
            // float z = Random.Range(-1f, 1f);
            // transform.position = startPosition + new Vector3(x, y, z) * shakeIntensity;

            // �ʱ� ��ġ�κ��� �� ����(Size 1) * shakeIntensity�� ���� �ȿ��� ī�޶� ��ġ ����
            transform.position = startPosition + UnityEngine.Random.insideUnitSphere * shakeIntensity;

            // �ð� ����
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        // ��鸮�� ������ ��ǥ ������ ����
        transform.position = startPosition;

        // ī�޶� ��鸲 ȿ�� ��� ����
        followCam.IsOnShake = false;
    }

    /// <summary>
    /// ī�޶� shakeTime ���� shakeIntensity�� ����� ���� �ڷ�ƾ �Լ�
    /// </summary>
    private IEnumerator ShakeByRotation()
    {
        // ī�޶� ��鸲 ȿ�� ��� ����
        followCam.IsOnShake = true;

        // ��鸮�� ������ ȸ�� ��
        Vector3 startRotation = transform.eulerAngles;
        // ȸ���� ��� shakeIntensity�� �� ū ���� �ʿ��ϱ� ������ ������ ����
        // (Ŭ���� ��� ������ ������ �ܺο��� ���� ����)
        float power = 10f;


        while (shakeTime > 0.0f)
        {
            // ȸ���ϱ� ���ϴ� �ุ �����ؼ� ��� (ȸ������ ���� ���� 0���� ����)
            float x = 0; // Random.Range(-1f, 1f);
            float y = 0; // Random.Range(-1f, 1f);
            float z = UnityEngine.Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            // �ð� ����
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        // ��鸮�� ������ ȸ�� ������ ����
        transform.rotation = Quaternion.Euler(startRotation);

        // ī�޶� ��鸲 ȿ�� ��� ����
        followCam.IsOnShake = false;
    }
}
