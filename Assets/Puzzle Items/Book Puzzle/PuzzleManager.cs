using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BookSocket[] bookSockets; // �鱾�������
    public CabinetController cabinet; // ���ù��ӿ��ƽű�
    public AudioSource audioSource; // ���ڲ�����Ч
    public AudioClip successSound; // �ɹ���Ч
    public AudioClip failureSound; // ʧ����Ч
    public GuideTextController guideTextController; // ����GuideTextController

    public void CheckSolution()
    {
        // ���ȼ�����е�BookSocket�Ƿ��Ѿ�����
        foreach (BookSocket socket in bookSockets)
        {
            if (!socket.IsFilled()) // ����BookSocket��һ������������Ƿ�����
            {
                return; // �����δ������socket��ֱ�ӷ���
            }
        }

        //StartCoroutine(UnlockPuzzleWithDelay(0.5f));

        // ����鱾��˳���Ƿ���ȷ
        foreach (BookSocket socket in bookSockets)
        {
            if (!socket.IsCorrectBook())
            {
                UnityEngine.Debug.Log("Puzzle failed. Books are out of order");
                PlayFailureSound(); // ����ʧ����Ч
                return;
            }
        }

        // ��������鱾����ȷ����������
        UnityEngine.Debug.Log("Puzzle solved! Books in correct order");
        UnlockPuzzle();
    }

    private IEnumerator UnlockPuzzleWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �ȴ�ָ����ʱ��

        UnlockPuzzle(); // ��������
    }

    private void UnlockPuzzle()
    {
        UnityEngine.Debug.Log("Puzzle solved successfully");

        // ���ųɹ���Ч
        if (audioSource != null && successSound != null)
        {
            audioSource.PlayOneShot(successSound);
        }
        else
        {
            UnityEngine.Debug.Log("AudioSource or SuccessSound is missing");
        }

        // ��������
        if (cabinet != null)
        {
            cabinet.Unlock(); // ��������
            cabinet.Open(); // �򿪹���
        }
        else
        {
            UnityEngine.Debug.Log("CabinetController reference is missing");
        }
        guideTextController.ShowGuideText("Looks like the cabinet's open");
    }

    private void PlayFailureSound()
    {
        // ����ʧ����Ч
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