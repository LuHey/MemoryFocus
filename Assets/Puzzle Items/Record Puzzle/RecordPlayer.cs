using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    private Record currentRecord; // ��ǰ���õĳ�Ƭ
    public CabinetController cabinet; // ���ù��ӿ��ƽű�
    //public AudioSource audioSource; // ���ڲ�����Ч
    public AudioClip successSound; // �ɹ���Ч
    public AudioClip failureSound; // ʧ����Ч
    //public GuideTextController guideTextController; // ����GuideTextController
    public StoryTrigger storyTrigger; // ����TriggerStory

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // ����XR������Select Entered�¼�
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // ��ȡ���õ�Record����
        Record record = args.interactableObject.transform.GetComponent<Record>();
        if (record != null)
        {
            SetCurrentRecord(record);
            PlayCurrentRecord();
            if (record.isTrueRecord)
            {
                if (cabinet != null)
                {
                    cabinet.Unlock(); // ��������
                    cabinet.Open(); // �򿪹���
                    if (audioSource != null && successSound != null)
                    {
                        audioSource.PlayOneShot(successSound);
                        //guideTextController.ShowGuideText("Looks like the cabinet's open");
                        //guideTextController.ShowGuideText("Looks like the cabinet's open");
                        storyTrigger.TriggerStory(); // ��������
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
        UnityEngine.Debug.Log("��ǰ����ĳ�Ƭ�ǣ�" + record.name);
    }

    public void PlayCurrentRecord()
    {
        if (currentRecord == null)
        {
            UnityEngine.Debug.LogWarning("û�з��ó�Ƭ��");
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
            UnityEngine.Debug.Log("��������: " + clip.name);
        }
        else
        {
            UnityEngine.Debug.LogWarning("�ó�Ƭû����Ƶ�ļ���");
        }
    }

    public void StopPlaying()
    {
        audioSource.Stop();
        currentRecord = null;
    }
}
