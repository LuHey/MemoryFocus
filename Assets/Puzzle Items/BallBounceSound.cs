using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BallBounceSound : MonoBehaviour
{
    private AudioSource audioSource;

    public float maxVolume = 1.0f; // 最大音量
    public float minImpactThreshold = 0.1f; // 最小撞击力度阈值
    public float maxImpactThreshold = 10.0f; // 最大撞击力度阈值

    void Start()
    {
        // 获取Audio Source组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            UnityEngine.Debug.LogWarning("AudioSource组件未找到，已自动添加一个AudioSource组件。");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactMagnitude = collision.relativeVelocity.magnitude;
        UnityEngine.Debug.Log("碰撞检测到，撞击力度: " + impactMagnitude);

        if (audioSource != null && impactMagnitude > minImpactThreshold)
        {
            // 根据撞击力度计算音量，并将其限制在 0 到 maxVolume 之间
            float volume = Mathf.Clamp((impactMagnitude - minImpactThreshold) / (maxImpactThreshold - minImpactThreshold), 0f, 1f);
            audioSource.volume = volume * maxVolume;

            UnityEngine.Debug.Log("播放声音，音量大小: " + audioSource.volume);
            audioSource.Play();
        }
    }
}

