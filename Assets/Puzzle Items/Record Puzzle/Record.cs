using UnityEngine;

public class Record : MonoBehaviour
{
    public AudioClip musicClip; // ��Ƶ�ļ�

    public bool isTrueRecord = false; // ��Ƶ�ļ�

    public AudioClip GetMusicClip()
    {
        return musicClip;
    }
}
