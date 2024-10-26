using System.Diagnostics;
using UnityEngine;

public class ToggleHelpMenu : MonoBehaviour
{
    public GameObject helpCanvas; // ��ק��Ҫ��ʾ/���ص�Canvas������

    // ������������л�Canvas����ʾ״̬
    public void ToggleHelp()
    {
        if (helpCanvas != null)
        {
            // �л�Canvas�ļ���״̬
            helpCanvas.SetActive(!helpCanvas.activeSelf);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Help Canvas is not assigned in the inspector.");
        }
    }
}
