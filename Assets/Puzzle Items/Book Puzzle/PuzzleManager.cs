using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSocket[] bookSockets; // 书本插槽数组
    public CabinetController cabinet; // 引用柜子控制脚本
    public AudioSource audioSource; // 用于播放音效
    public AudioClip successSound; // 成功音效
    public AudioClip failureSound; // 失败音效
    public GuideTextController guideTextController; // 引用GuideTextController

    public void CheckSolution()
    {
        // 首先检查所有的BookSocket是否已经插满
        foreach (BookSocket socket in bookSockets)
        {
            if (!socket.IsFilled()) // 假设BookSocket有一个方法来检查是否有书
            {
                return; // 如果有未填满的socket，直接返回
            }
        }

        //StartCoroutine(UnlockPuzzleWithDelay(0.5f));

        // 检查书本的顺序是否正确
        foreach (BookSocket socket in bookSockets)
        {
            if (!socket.IsCorrectBook())
            {
                UnityEngine.Debug.Log("Puzzle failed. Books are out of order");
                PlayFailureSound(); // 播放失败音效
                return;
            }
        }

        // 如果所有书本都正确，解锁谜题
        UnityEngine.Debug.Log("Puzzle solved! Books in correct order");
        UnlockPuzzle();
    }

    private IEnumerator UnlockPuzzleWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的时间

        UnlockPuzzle(); // 解锁谜题
    }

    private void UnlockPuzzle()
    {
        UnityEngine.Debug.Log("Puzzle solved successfully");

        // 播放成功音效
        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound);
        }
        else
        {
            UnityEngine.Debug.Log("AudioSource or SuccessSound is missing");
        }

        // 解锁柜子
        if (cabinet != null)
        {
            cabinet.Unlock(); // 解锁柜子
            cabinet.Open(); // 打开柜子
        }
        else
        {
            UnityEngine.Debug.Log("CabinetController reference is missing");
        }
        guideTextController.ShowGuideText("Looks like the cabinet's open");
    }

    private void PlayFailureSound()
    {
        // 播放失败音效
        if (audioSource != null && failureSound != null)
        {
            audioSource.PlayOneShot(failureSound);
        }
        else
        {
            UnityEngine.Debug.Log("AudioSource or FailureSound is missing");
        }
    }
}