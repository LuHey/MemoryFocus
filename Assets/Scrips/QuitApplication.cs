using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class QuitApplication : MonoBehaviour
{
    // ������������˳���Ϸ
    public void QuitGame()
    {
        // �ڱ༭��ģʽ�£�ֹͣ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �ڹ�������Ϸ�У��˳�Ӧ�ó���
        UnityEngine.Application.Quit();
#endif
    }
}
