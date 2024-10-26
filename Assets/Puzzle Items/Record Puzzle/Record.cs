using UnityEngine;

public class Record : MonoBehaviour
{
    public AudioClip musicClip; // 音频文件

    public bool isTrueRecord = false; // 音频文件

    public AudioClip GetMusicClip()
    {
        return musicClip;
    }
}
