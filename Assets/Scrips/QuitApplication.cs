using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class QuitApplication : MonoBehaviour
{
    // 这个方法用于退出游戏
    public void QuitGame()
    {
        // 在编辑器模式下，停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在构建的游戏中，退出应用程序
        UnityEngine.Application.Quit();
#endif
    }
}
