using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    private Record currentRecord; // 当前放置的唱片
    public CabinetController cabinet; // 引用柜子控制脚本
    //public AudioSource audioSource; // 用于播放音效
    public AudioClip successSound; // 成功音效
    public AudioClip failureSound; // 失败音效
    //public GuideTextController guideTextController; // 引用GuideTextController
    public StoryTrigger storyTrigger; // 引用TriggerStory

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 用于XR交互的Select Entered事件
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 获取放置的Record对象
        Record record = args.interactableObject.transform.GetComponent<Record>();
        if (record != null)
        {
            SetCurrentRecord(record);
            PlayCurrentRecord();
            if (record.isTrueRecord)
            {
                if (cabinet != null)
                {
                    cabinet.Unlock(); // 解锁柜子
                    cabinet.Open(); // 打开柜子
                    if (audioSource != null && successSound != null)
                    {
                        audioSource.PlayOneShot(successSound);
                        //guideTextController.ShowGuideText("Looks like the cabinet's open");
                        //guideTextController.ShowGuideText("Looks like the cabinet's open");
                        storyTrigger.TriggerStory(); // 触发剧情
                        //guideTextController.
                    }

                }
                else
                {
                    UnityEngine.Debug.Log("CabinetController reference is missing");
                }
            }
        }
    }

    public void SetCurrentRecord(Record record)
    {
        currentRecord = record;
        UnityEngine.Debug.Log("当前放入的唱片是：" + record.name);
    }

    public void PlayCurrentRecord()
    {
        if (currentRecord == null)
        {
            UnityEngine.Debug.LogWarning("没有放置唱片。");
            return;
        }

        AudioClip clip = currentRecord.GetMusicClip();
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.spatialize = true;
            audioSource.spatialBlend = 1;
            audioSource.Play();
            UnityEngine.Debug.Log("播放音乐: " + clip.name);
        }
        else
        {
            UnityEngine.Debug.LogWarning("该唱片没有音频文件。");
        }
    }

    public void StopPlaying()
    {
        audioSource.Stop();
        currentRecord = null;
    }
}
