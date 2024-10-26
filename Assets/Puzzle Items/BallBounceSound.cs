using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BallBounceSound : MonoBehaviour
{
    private AudioSource audioSource;

    public float maxVolume = 1.0f; // �������
    public float minImpactThreshold = 0.1f; // ��Сײ��������ֵ
    public float maxImpactThreshold = 10.0f; // ���ײ��������ֵ

    void Start()
    {
        // ��ȡAudio Source���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            UnityEngine.Debug.LogWarning("AudioSource���δ�ҵ������Զ����һ��AudioSource�����");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactMagnitude = collision.relativeVelocity.magnitude;
        UnityEngine.Debug.Log("��ײ��⵽��ײ������: " + impactMagnitude);

        if (audioSource != null && impactMagnitude > minImpactThreshold)
        {
            // ����ײ�����ȼ��������������������� 0 �� maxVolume ֮��
            float volume = Mathf.Clamp((impactMagnitude - minImpactThreshold) / (maxImpactThreshold - minImpactThreshold), 0f, 1f);
            audioSource.volume = volume * maxVolume;

            UnityEngine.Debug.Log("����������������С: " + audioSource.volume);
            audioSource.Play();
        }
    }
}

